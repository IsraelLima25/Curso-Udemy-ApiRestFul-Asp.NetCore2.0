using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using CrudRestAspNetCore.Model;
using CrudRestAspNetCore.Repository;
using CrudRestAspNetCore.Security.Configuration;

namespace CrudRestAspNetCore.Business.Implementations
{
    public class LoginBusinessImpl : ILoginBusiness
    {
        private IUserRepository repository;
        private SigningConfigurations SigningConfigurations;
        private TokenConfiguration TokenConfiguration;


        public LoginBusinessImpl(IUserRepository repository, SigningConfigurations signingConfigurations,
            TokenConfiguration tokenConfiguration)
        {
            this.repository = repository;
            this.SigningConfigurations = signingConfigurations;
            this.TokenConfiguration = tokenConfiguration;
        }

        public object FindByLogin(User user)
        {
            bool credentialsIsValid = false;
            if (user != null && !string.IsNullOrWhiteSpace(user.login))
            {
                var baseUser = repository.FindByLogin(user.login);
                credentialsIsValid = (baseUser != null && user.login == baseUser.login &&
                    user.AcessKey == baseUser.AcessKey);
            }
            if (credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.login, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName,user.login)

                    }
                    );
                //this.TokenConfiguration.Second
                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(12000);
                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);
                return SucessObject(createDate, expirationDate, token);
            }
            else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = this.TokenConfiguration.Issuer,
                Audience = this.TokenConfiguration.Audience,
                SigningCredentials = this.SigningConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate

            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to authenticate"
            };
        }

        private object SucessObject(DateTime createDate, DateTime expirationDate, String token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                message = "OK"
            };
        }
    }
}
