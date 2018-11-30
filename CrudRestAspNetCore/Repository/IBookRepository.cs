using CrudRestAspNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Repository
{
    public interface IBookRepository
    {
        Book Create(Book person);
        Book FindById(int id);
        List<Book> FindAll();
        Book Update(Book person);
        void Delete(string id);
    }
}
