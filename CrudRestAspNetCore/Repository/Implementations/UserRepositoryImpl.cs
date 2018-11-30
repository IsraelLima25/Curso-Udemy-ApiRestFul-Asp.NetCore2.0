using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudRestAspNetCore.Business;
using CrudRestAspNetCore.Model;
using CrudRestAspNetCore.Model.Context;
using CrudRestAspNetCore.Repository.Generic;

namespace CrudRestAspNetCore.Repository.Implementations

{
    public class UserRepositoryImpl : IUserRepository
    {
        private MySqlContext context;

        public UserRepositoryImpl(MySqlContext context)
        {
            this.context = context;
        }

        public User FindByLogin(string login)
        {
            return this.context.user.SingleOrDefault(u => u.login.Equals(login));

        }
    }
}
