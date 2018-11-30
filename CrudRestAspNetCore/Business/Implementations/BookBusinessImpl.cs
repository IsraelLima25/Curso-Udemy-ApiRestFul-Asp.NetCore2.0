using System;
using System.Collections.Generic;
using CrudRestAspNetCore.Data.Converters;
using CrudRestAspNetCore.Data.Vo;
using CrudRestAspNetCore.Model;
using CrudRestAspNetCore.Repository;
using CrudRestAspNetCore.Repository.Generic;

namespace CrudRestAspNetCore.Business.Implementations
{
    public class BookBusinessImpl : IBookBusiness
    {
        private IRepository <Book> repository;
        private BookConverter converter;

        public BookBusinessImpl(IRepository<Book> repository)
        {
            this.repository = repository;
            this.converter = new BookConverter();
        }

        public BookVo Create(BookVo book)
        {
            var entityBook = this.converter.Parse(book);
            entityBook = this.repository.Create(entityBook);
            return this.converter.Parse(entityBook);
        }
      
        public void Delete(int id)
        {
            this.repository.Delete(id);
        }

        public List<BookVo> FindAll()
        {
            return this.converter.ParserList(this.repository.FindAll());
        }

        public BookVo FindById(int id)
        {
            return this.converter.Parse(this.repository.FindById(id));
        }     

        public BookVo Update(BookVo book)
        {
            var entityBook = this.converter.Parse(book);
            entityBook = this.repository.Update(entityBook);
            return this.converter.Parse(entityBook);
        }
    }
}
