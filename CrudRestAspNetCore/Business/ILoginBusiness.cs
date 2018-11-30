using CrudRestAspNetCore.Data.Vo;
using CrudRestAspNetCore.Model;
using System.Collections.Generic;

namespace CrudRestAspNetCore.Business
{
    public interface ILoginBusiness
    {
        
        object FindByLogin(User user);
              

    }
}
