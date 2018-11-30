using System;
using CrudRestAspNetCore.Model.Context;
using CrudRestAspNetCore.Business;
using CrudRestAspNetCore.Business.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CrudRestAspNetCore.Repository.Generic;
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using CrudRestAspNetCore.HyperMedia;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using CrudRestAspNetCore.Security.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using CrudRestAspNetCore.Repository;
using CrudRestAspNetCore.Repository.Implementations;

namespace CrudRestAspNetCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger logger;
        public IHostingEnvironment environment { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            Configuration = configuration;
            this.environment = environment;
            this.logger = logger;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connectionString));
            //Depois de adicionar a dependencia no nuget, para suporte a arquivos xml, configura como
            //uma option no service.AddMvc.

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("tex/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("tex/json"));
            }).AddXmlSerializerFormatters().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());

            //Configurações para utilização do Migration Evolve
            if (this.environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                }
                catch (Exception e)
                {
                    this.logger.LogCritical("DataBase Migration Failed", e);
                    throw;
                }
            }

            //Utilização do Swagger Versionamento de Api            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("api",
                    new Info
                    {
                        Title = "Restful API With Asp.Net Core 2.0",
                        Version = "api"
                    });
            });

            //Dependency Injector
            services.AddScoped<ILoginBusiness, LoginBusinessImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IUserRepository, UserRepositoryImpl>();
            services.AddScoped<IBookBusiness, BookBusinessImpl>();
            //services.AddScoped<IBookRepository, BookRepositoryImpl>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddSingleton(filterOptions);

            //Token Configuration

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                this.Configuration.GetSection("TokenConfiguration")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Para renderizar na página inical
            app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/api/swagger.json", "My API");

            });

            var options = new RewriteOptions();
            options.AddRedirect("^$", "swagger");
            app.UseRewriter(options);
            //Adicionando rota para utilizar Hateoas
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}"
                        );
            });
        }
    }
}
