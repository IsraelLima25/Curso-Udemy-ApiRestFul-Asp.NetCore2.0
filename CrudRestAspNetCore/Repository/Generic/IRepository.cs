using CrudRestAspNetCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(int id);
        List<T> FindAll();
        T Update(T item);
        void Delete(int id);
        bool Exist(int? id);
        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
