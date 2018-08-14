namespace ShishaFlavours.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShishaFlavours.Services.Interfaces;
    using ShishaFlavoursAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using ShishaFlavours.Services.ResponseModels;

    public class FlavourController : Controller
    {
        private readonly IFlavoursService flavoursService = null;

        public FlavourController(IFlavoursService flavoursService)
        {
            this.flavoursService = flavoursService;
        }

        [Authorize]
        [HttpGet]
        public async Task<Flavour> GetFlavour(string Name)
        {
            return await flavoursService.GetFlavourByName(Name);
        }

        [HttpGet]
        public async Task<List<Flavour>> GetFlavours()
        {
            return (await flavoursService.GetAllFlavours()).ToList();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string Name)
        {
            ResultStatus status = await flavoursService.CreateFlavour(Name);

            return new JsonResult(status);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(string Name)
        {
            ResultStatus status = await flavoursService.DeleteFlavourByName(Name);

            return new JsonResult(status);
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> Update(string Name, string NewName)
        {
            ResultStatus status = await flavoursService.UpdateFlavourByName(Name, NewName);

            return new JsonResult(status);
        }
    }
}