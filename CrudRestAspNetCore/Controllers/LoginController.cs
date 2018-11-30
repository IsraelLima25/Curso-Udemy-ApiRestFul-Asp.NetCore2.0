using CrudRestAspNetCore.Business;
using Microsoft.AspNetCore.Mvc;
using Tapioca.HATEOAS;
using Microsoft.AspNetCore.Authorization;
using CrudRestAspNetCore.Model;

namespace CrudRestAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private ILoginBusiness loginBusiness;

        public LoginController(ILoginBusiness loginBusiness)
        {
            this.loginBusiness = loginBusiness;
        }    

        // POST api/values
        [AllowAnonymous]
        [HttpPost]       
        public object Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            return this.loginBusiness.FindByLogin(user);
        }

      
    }
}
