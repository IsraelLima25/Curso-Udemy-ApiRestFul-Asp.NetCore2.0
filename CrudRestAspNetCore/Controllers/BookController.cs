using CrudRestAspNetCore.Business;
using CrudRestAspNetCore.Data.Vo;
using CrudRestAspNetCore.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {

        private IBookBusiness bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
            this.bookBusiness = bookBusiness;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(this.bookBusiness.FindAll());


        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = this.bookBusiness.FindById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] BookVo book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            return new ObjectResult(this.bookBusiness.Create(book));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] BookVo book)
        {
            if (book == null)
            {
                NotFound();
            }
            return new ObjectResult(this.bookBusiness.Update(book));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.bookBusiness.Delete(id);
            return NoContent();
        }


    }
}
