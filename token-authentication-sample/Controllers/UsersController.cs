using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenAuthenticationSample.Security;

namespace token_authentication_sample.Controllers {
    [Authorize, Route("api/[controller]")]
    public class UsersController : Controller {
        // GET api/values
        [AllowAnonymous, HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) {
            var user = HttpContext.User;
            return "Id: " + user.Identifier() + "<br/>Surname: " + user.Surname();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value) {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
