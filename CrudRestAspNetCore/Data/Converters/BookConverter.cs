using CrudRestAspNetCore.Data.Converter;
using CrudRestAspNetCore.Data.Vo;
using CrudRestAspNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Data.Converters
{
    public class BookConverter : IParse<Book, BookVo>, IParse<BookVo, Book>
    {
        public Book Parse(BookVo origin)
        {
            if(origin == null)
            {
                return null;
            }
            else
            {
                Book book = new Book();
                book.id = origin.id;
                book.Title = origin.Title;
                book.Author = origin.Author;
                book.Price = origin.Price;
                book.LaunchDate = origin.LaunchDate;
                return book;
            }
        }

        public BookVo Parse(Book origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                BookVo book = new BookVo();
                book.id = origin.id;
                book.Title = origin.Title;
                book.Author = origin.Author;
                book.Price = origin.Price;
                book.LaunchDate = origin.LaunchDate;
                return book;
            }
        }

        public List<Book> ParserList(List<BookVo> origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return origin.Select(item => Parse(item)).ToList();
            }
        }

        public List<BookVo> ParserList(List<Book> origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return origin.Select(item => Parse(item)).ToList();
            }
        }
    }
}
