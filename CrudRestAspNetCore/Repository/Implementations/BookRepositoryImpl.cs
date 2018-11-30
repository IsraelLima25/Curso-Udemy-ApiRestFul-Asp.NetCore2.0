using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudRestAspNetCore.Model;
using CrudRestAspNetCore.Model.Context;
using CrudRestAspNetCore.Repository.Generic;

namespace CrudRestAspNetCore.Repository.Implementations
{
    public class BookRepositoryImpl : IRepository<Book>
    {

        private MySqlContext context;

        public BookRepositoryImpl(MySqlContext context)
        {
            this.context = context;
        }

        public Book Create(Book book)
        {
            try
            {
                this.context.Add(book);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return book;
        }

        public void Delete(int id)
        {
            var result = this.context.book.SingleOrDefault(p => p.id.Equals(id));

            try
            {
                if (result != null)
                {
                    this.context.book.Remove(result);
                    this.context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool Exist(int? id)
        {
            return this.context.book.Any(p => p.id.Equals(id));
        }

        public List<Book> FindAll()
        {
            return this.context.book.ToList();

            //return this.context.person.ToList();
        }

        public Book FindById(int id)
        {
            return this.context.book.SingleOrDefault(p => p.id.Equals(id));
        }

        public List<Book> FindWithPagedSearch(string query)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string query)
        {
            throw new NotImplementedException();
        }

        public Book Update(Book book)
        {
            if (!Exist(book.id)) return new Book();
            var result = this.context.person.SingleOrDefault(p => p.id.Equals(book.id));
            try
            {
                this.context.Entry(result).CurrentValues.SetValues(book);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return book;
        }
    }
}
