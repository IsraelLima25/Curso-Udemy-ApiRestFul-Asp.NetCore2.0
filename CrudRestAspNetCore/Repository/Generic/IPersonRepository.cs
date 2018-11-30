using CrudRestAspNetCore.Model;
using CrudRestAspNetCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Repository.Generic
{
    public interface IPersonRepository : IRepository<PersonVO>
    {       
        List<PersonVO> FindByName(string firstName,string lastName);
       
    }
}
