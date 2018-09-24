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

    public class FlavoursController : Controller
    {
        private readonly IFlavoursService flavoursService = null;

        public FlavoursController(IFlavoursService flavoursService)
        {
            this.flavoursService = flavoursService;
        }

        [Authorize]
        [HttpGet]
        public async Task<Flavour> GetFlavour(int id)
        {
            return await flavoursService.GetFlavourById(id);
        }

        [HttpGet]
        public async Task<List<Flavour>> All()
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
        public async Task<IActionResult> Delete(int id)
        {
            ResultStatus status = await flavoursService.DeleteFlavourById(id);

            return new JsonResult(status);
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> Update(int id, string NewName)
        {
            ResultStatus status = await flavoursService.UpdateFlavourById(id, NewName);

            return new JsonResult(status);
        }
    }
}