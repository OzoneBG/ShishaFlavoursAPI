namespace ShishaFlavoursAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShishaFlavoursAPI.Models;
    using ShishaFlavoursAPI.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ValuesController : ControllerBase
    {
        private UserManager<User> userManager;
        private IFlavoursService flavoursService;

        public ValuesController(UserManager<User> userManager, IFlavoursService flavoursService)
        {
            this.userManager = userManager;
            this.flavoursService = flavoursService;
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
        public async Task<IActionResult> Index()
        {
            var flavours = await flavoursService.GetAllFlavours() as List<Flavour>;
            if(flavours.Count == 0)
            {
                return NotFound("No flavours found");
            }
            return new JsonResult(flavours);
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
