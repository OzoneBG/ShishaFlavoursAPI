namespace ShishaFlavours.Services
{
    using Microsoft.EntityFrameworkCore;
    using ShishaFlavours.Models.Relationships;
    using ShishaFlavours.Services.Interfaces;
    using ShishaFlavours.Services.ResponseModels;
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

        public async Task<ResultStatus> CreateFlavourCombinationAsync(FlavourCombination flavourCombination, List<Flavour> flavours)
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

            int code = await flavourCombinationRepository.SaveChanges();

            bool status = code != 0;

            ResultStatus resultStatus = new ResultStatus()
            {
                Status = status
            };

            if(status)
            {
                resultStatus.Message = "Successfully created a new combination";
            }
            else
            {
                resultStatus.Message = "Failed to create a new combination";
            }

            return resultStatus;
        }

        public async Task<ResultStatus> DeleteFlavourCombination(string name)
        {
            FlavourCombination target = await GetFlavourCombinationByName(name);
            flavourCombinationRepository.Delete(target);
            int code = await flavourCombinationRepository.SaveChanges();

            bool status = (code != 0);

            ResultStatus resultStatus = new ResultStatus
            {
                Status = status
            };

            if (status)
            {
                resultStatus.Message = "Successfully deleted flavour";
            }
            else
            {
                resultStatus.Message = "Failed to deleted flavour";
            }

            return resultStatus;
        }

        public async Task<ICollection<FlavourCombination>> GetAllCombinations()
        {
            return await flavourCombinationRepository.All().ToListAsync();
        }

        public async Task<FlavourCombination> GetFlavourCombinationByName(string name)
        {
            return await flavourCombinationRepository.All().Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<ResultStatus> UpdateFlavourCombination(string name, string newName)
        {
            FlavourCombination target = await GetFlavourCombinationByName(name);
            target.Name = newName;
            flavourCombinationRepository.Update(target);
            int code = await flavourCombinationRepository.SaveChanges();

            bool status = (code != 0);

            ResultStatus resultStatus = new ResultStatus
            {
                Status = status
            };

            if (status)
            {
                resultStatus.Message = "Successfully updated flavour combination";
            }
            else
            {
                resultStatus.Message = "Failed to update flavour combination";
            }

            return resultStatus;
        }
    }
}
