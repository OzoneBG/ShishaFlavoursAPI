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

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            User user = new User
            {
                UserName = "Young",
                Email = "muzunov@hotmail.com"
            };

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            IdentityResult result = await userManager.CreateAsync(user, "1q2w3e$R");

            if (result.Succeeded)
            {
                return new JsonResult("Created user");
            }
            else
            {
                return new JsonResult(result.Errors);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                User userInDB = await userManager.FindByNameAsync("Young");

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
