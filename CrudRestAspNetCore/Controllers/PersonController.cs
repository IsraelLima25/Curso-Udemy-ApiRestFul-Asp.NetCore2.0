using CrudRestAspNetCore.Model;
using CrudRestAspNetCore.Business;
using Microsoft.AspNetCore.Mvc;
using CrudRestAspNetCore.Data.Vo;
using Tapioca.HATEOAS;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using Microsoft.AspNetCore.Authorization;

namespace CrudRestAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private IPersonBusiness personBusiness;

        public PersonController(IPersonBusiness personBusiness)
        {
            this.personBusiness = personBusiness;
        }

        // GET api/values
        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<PersonVo>))]
        [Authorize("Bearer")]
        public IActionResult Get()
        {
            return Ok(this.personBusiness.FindAll());
        }

        //Implementação Query param
        [HttpGet("find-by-name")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<PersonVo>))]
        [Authorize("Bearer")]
        public IActionResult GetByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            return Ok(this.personBusiness.FindByName(firstName, lastName));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(PersonVo))]
        [ProducesResponseType(404)]
        [Authorize("Bearer")]
        public IActionResult Get(int id)
        {
            var person = this.personBusiness.FindById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        //Implementação Query param
        [HttpGet("find-with-paged-search/{sortDirection}/{pageSize}/{page}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<PersonVo>))]
        [Authorize("Bearer")]
        public IActionResult GetPageSearch([FromQuery]string name, string sortDirection, int pageSize, int page)
        {
            return Ok(this.personBusiness.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }

        // POST api/values
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Post([FromBody] PersonVo person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            return new ObjectResult(this.personBusiness.Create(person));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Put([FromBody] PersonVo person)
        {
            if (person == null)
            {
                NotFound();
            }
            return new ObjectResult(this.personBusiness.Update(person));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [Authorize("Bearer")]
        public IActionResult Delete(int id)
        {
            this.personBusiness.Delete(id);
            return NoContent();
        }
    }
}
