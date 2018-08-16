namespace ShishaFlavoursAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ShishaFlavours.Services.Interfaces;
    using ShishaFlavoursAPI.Models;
    using System.Threading.Tasks;

    public class ValuesController : ControllerBase
    {
        private UserManager<User> userManager;
        private IFlavoursService flavoursService;
        private IFlavourCombinationsService flavourCombinationsService;

        public ValuesController(UserManager<User> userManager, IFlavoursService flavoursService, IFlavourCombinationsService flavourCombinationsService)
        {
            this.userManager = userManager;
            this.flavoursService = flavoursService;
            this.flavourCombinationsService = flavourCombinationsService;
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
            var flavourCombination = await flavourCombinationsService.GetFlavourCombinationByName("Sweet Mint");
            if(flavourCombination == null)
            {
                return NotFound("No flavours found");
            }
            return new JsonResult(flavourCombination);
        }
    }
}
