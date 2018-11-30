using CrudRestAspNetCore.Model;
using System.Collections.Generic;

namespace CrudRestAspNetCore.Repository
{
    public interface IUserRepository
    {
       
        User FindByLogin(string login);   
        

    }
}
