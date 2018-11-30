using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Model.Context
{
    public class MySqlContext : DbContext
    {

        public MySqlContext()
        {

        }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {

        }

        //Deve ser corresponte ao nome da tabela
        public DbSet<PersonVO> person { get; set; }
        public DbSet<Book> book { get; set; }
        public DbSet<User> user { get; set; }
    }
}
