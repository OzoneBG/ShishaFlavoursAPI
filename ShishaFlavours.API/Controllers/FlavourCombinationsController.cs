namespace ShishaFlavours.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShishaFlavours.Services.Interfaces;
    using System.Threading.Tasks;

    public class FlavourCombinationsController : Controller
    {
        private readonly IFlavoursService flavoursService = null;
        private readonly IFlavourCombinationsService flavourCombinationsService = null;

        public FlavourCombinationsController(IFlavoursService flavoursService, IFlavourCombinationsService flavourCombinationsService)
        {
            this.flavoursService = flavoursService;
            this.flavourCombinationsService = flavourCombinationsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return null;
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> Update()
        {
            return null;
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return null;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            return null;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFlavourCombination()
        {
            return null;
        }
    }
}