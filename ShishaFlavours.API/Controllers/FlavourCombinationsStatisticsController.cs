namespace ShishaFlavours.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShishaFlavours.Services.Interfaces;
    using System.Threading.Tasks;

    public class FlavourCombinationsStatisticsController : Controller
    {
        private readonly IFlavourCombinationsService flavourCombinationsService = null;

        public FlavourCombinationsStatisticsController(IFlavourCombinationsService flavourCombinationsService)
        {
            this.flavourCombinationsService = flavourCombinationsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFlavourCombinationContaining(int flavourId)
        {
            var flavourCombosContainingFlavour = await flavourCombinationsService.GetFlavourCombinationsContaining(flavourId);

            return new JsonResult(flavourCombosContainingFlavour);
        }

        [HttpGet]
        public async Task<IActionResult> GetTop10FlavourCombinations()
        {
            var top10 = await flavourCombinationsService.GetTop10Combinations();

            return new JsonResult(top10);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCombinationsByVotes(bool isDescending = true)
        {
            var combosByVotes = await flavourCombinationsService.GetCombinationsByVotes(isDescending);

            return new JsonResult(combosByVotes);
        }
    }
}