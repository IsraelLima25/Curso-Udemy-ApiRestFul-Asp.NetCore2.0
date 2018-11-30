using CrudRestAspNetCore.Data.Vo;
using CrudRestAspNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Business
{
    public interface IBookBusiness
    {
        BookVo Create(BookVo person);
        BookVo FindById(int id);
        List<BookVo> FindAll();
        BookVo Update(BookVo person);
        void Delete(int id);
    }
}
