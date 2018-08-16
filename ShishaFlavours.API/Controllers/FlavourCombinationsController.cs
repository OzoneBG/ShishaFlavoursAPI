namespace ShishaFlavours.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShishaFlavours.API.RequestModels.FlavourCombination;
    using ShishaFlavours.Models.Relationships;
    using ShishaFlavours.Services.Interfaces;
    using ShishaFlavours.Services.ResponseModels;
    using ShishaFlavoursAPI.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

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
        public async Task<IActionResult> Create([FromBody] FlavourCombinationCreateModel createModel)
        {
            IActionResult response = null;

            if(ModelState.IsValid)
            {
                FlavourCombination flavourCombo = new FlavourCombination()
                {
                    Name = createModel.Name,
                    Description = createModel.Description,
                    DateAdded = DateTime.Now,
                    UserId = createModel.UserId,
                    FlavourCombinationReferences = new List<FlavourCombinationReference>()
                };

                List<Flavour> flavours = new List<Flavour>();

                foreach (string flavourName in createModel.Flavours)
                {
                    Flavour flavour = await flavoursService.GetFlavourByName(flavourName);
                    flavours.Add(flavour);
                }

                ResultStatus status = await flavourCombinationsService.CreateFlavourCombinationAsync(flavourCombo, flavours);
                response = new JsonResult(status);
            }
            else
            {
                return BadRequest(ModelState);
            }

            return response;
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> Update(string name, string newName)
        {
            ResultStatus status = await flavourCombinationsService.UpdateFlavourCombination(name, newName);
            return new JsonResult(status);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            ResultStatus status = await flavourCombinationsService.DeleteFlavourCombination(name);
            return new JsonResult(status);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allCombinations = (await flavourCombinationsService.GetAllCombinations()).ToList();
            return new JsonResult(allCombinations);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFlavourCombination(string Name)
        {
            FlavourCombination model = await flavourCombinationsService.GetFlavourCombinationByName(Name);

            return new JsonResult(model);
        }
    }
}