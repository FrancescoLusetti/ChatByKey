using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBroker.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ApiBroker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/Users/SignUp
        [HttpPost("SignUp")]
        public void SignUp([FromForm] string username, [FromForm] string password)
        {
            UsersHelper.InsertUser("dope", "swag");
        }

        // POST api/Users/Login
        [HttpPost("Login")]
        public void Login([FromForm] string username, [FromForm] string password)
        {
            UsersHelper.Login("dope", "swag");
        }
    }
}
