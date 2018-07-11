namespace ShishaFlavoursAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShishaFlavoursAPI.Models;
    using System.Threading.Tasks;

    public class ValuesController : ControllerBase
    {
        private UserManager<User> userManager;

        public ValuesController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser(string name)
        {
            if (User.Identity.IsAuthenticated)
            {
                User userInDB = await userManager.FindByNameAsync(name);

                if (userInDB == null)
                {
                    return NotFound("Didn't find user");
                }

                return new JsonResult(userInDB);
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET api/values
        [HttpGet]
        public IActionResult Index()
        {
            return new JsonResult(new string[] { "value1", "value2" });
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}
        //
        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}
        //
        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}
        //
        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
