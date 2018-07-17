namespace ShishaFlavours.Services
{
    using Microsoft.EntityFrameworkCore;
    using ShishaFlavours.API.ResultModel;
    using ShishaFlavours.Models.Relationships;
    using ShishaFlavours.Services.Interfaces;
    using ShishaFlavoursAPI.Data.Common.Repository;
    using ShishaFlavoursAPI.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FlavourCombinationService : IFlavourCombinationsService
    {
        private IRepository<FlavourCombination> flavourCombinationRepository = null;

        public FlavourCombinationService(IRepository<FlavourCombination> flavourCombinationRepository)
        {
            this.flavourCombinationRepository = flavourCombinationRepository;
        }

        public async Task CreateFlavourCombinationAsync(FlavourCombination flavourCombination, List<Flavour> flavours)
        {
            this.flavourCombinationRepository.Add(flavourCombination);

            foreach (Flavour flavour in flavours)
            {
                FlavourCombinationReference reference = new FlavourCombinationReference()
                {
                    FlavourCombinationId = flavourCombination.Id,
                    FlavourId = flavour.Id
                };

                flavourCombination.FlavourCombinationReferences.Add(reference);
            }

            await flavourCombinationRepository.SaveChanges();
        }

        public async Task<FlavourCombinationResultModel> GetFlavourCombinationByName(string name)
        {
            return await flavourCombinationRepository
                .All()
                .Select(t => new FlavourCombinationResultModel()
                {
                    Name = t.Name,
                    UserName = t.User.UserName,
                    flavours = t.FlavourCombinationReferences.Select(x => x.Flavour.Name).ToList()
                }).FirstOrDefaultAsync();
        }
    }
}
