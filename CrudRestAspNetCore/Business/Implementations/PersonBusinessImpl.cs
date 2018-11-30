using System.Collections.Generic;
using CrudRestAspNetCore.Data.Converters;
using CrudRestAspNetCore.Data.Vo;
using CrudRestAspNetCore.Model;
using CrudRestAspNetCore.Repository;
using CrudRestAspNetCore.Repository.Generic;
using Tapioca.HATEOAS.Utils;

namespace CrudRestAspNetCore.Business.Implementations
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IPersonRepository repository;
        private readonly PersonConverter converter;

        public PersonBusinessImpl(IPersonRepository repository)
        {
            this.repository = repository;
            this.converter = new PersonConverter();
        }

        public PersonVo Create(PersonVo person)
        {
            var personEntity = this.converter.Parse(person);
            personEntity = this.repository.Create(personEntity);
            return this.converter.Parse(personEntity);
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);

        }

        public List<PersonVo> FindAll()
        {

            return this.converter.ParserList(this.repository.FindAll());

        }

        public PersonVo FindById(int id)
        {
            return this.converter.Parse(this.repository.FindById(id));
        }
        /*
        PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            string query = @"select * from person p where";
            if (!string.IsNullOrEmpty(name)) query = query + $"and p.name like '%{name}%";
            query = query + $"order by p.name{sortDirection} limit {pageSize} offset{page}";
            string countQuery = @"select * from person p where";
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $"and p.name like '%{name}%";


            var persons = this.converter.ParserList(this.repository.FindWithPagedSearch(query));
            int totalResults = this.repository.GetCount(query);
            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page,
                List = this.converter.ParserList(persons),
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults

            };
        }
        */
        public PersonVo Update(PersonVo person)
        {
            var personEntity = this.converter.Parse(person);
            personEntity = this.repository.Update(personEntity);
            return this.converter.Parse(personEntity);

        }

        public List<PersonVo> FindByName(string firstName, string lastName)
        {
            return this.converter.ParserList(this.repository.FindByName(firstName, lastName));
        }

        PagedSearchDTO<PersonVo> IPersonBusiness.FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            string query = @"select * from person p where 1=1 ";
            if (!string.IsNullOrEmpty(name)) query = query + $" and p.firstname like'%{name}%'";
            query = query + $" order by p.firstname {sortDirection} limit {pageSize} offset {page} ";
            string countQuery = @" select * from person p where ";
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $" and p.firstname like '%{name}%'";


            var persons = this.converter.ParserList(this.repository.FindWithPagedSearch(query));
            int totalResults = this.repository.GetCount(query);

            return new PagedSearchDTO<PersonVo>
            {
                CurrentPage = page,
                List = persons,
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults

            };

        }
    }
}
