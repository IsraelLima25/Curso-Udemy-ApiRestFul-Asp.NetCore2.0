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
    public class PersonRepositoryImpl : GenericRepository<PersonVO>, IPersonRepository
    {

        public PersonRepositoryImpl(MySqlContext context) : base(context) { }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return this.context.person.Where(p => p.FirstName.Contains(firstName) && p.LastName.Contains(lastName)).ToList();

            }
            else if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return this.context.person.Where(p => p.LastName.Contains(lastName)).ToList();

            }
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return this.context.person.Where(p => p.FirstName.Contains(firstName)).ToList();

            }
            else
            {
                return this.context.person.ToList();
            }

        }
    }
}
