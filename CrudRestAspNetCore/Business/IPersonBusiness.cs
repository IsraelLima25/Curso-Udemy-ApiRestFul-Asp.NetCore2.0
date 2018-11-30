using CrudRestAspNetCore.Data.Vo;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace CrudRestAspNetCore.Business
{
    public interface IPersonBusiness
    {
        PersonVo Create(PersonVo person);
        PersonVo FindById(int id);
        List<PersonVo> FindAll();
        List<PersonVo> FindByName(string firstName, string lastName);
        PersonVo Update(PersonVo person);
        void Delete(int id);
        PagedSearchDTO<PersonVo> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

    }
}
