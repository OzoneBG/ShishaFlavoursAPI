﻿namespace ShishaFlavours.Services
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

        public async Task<ResultStatus> DeleteFlavourCombination(int id)
        {
            FlavourCombination target = await GetFlavourCombinationById(id);
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

        public async Task<ICollection<FlavourCombination>> GetCombinationsByVotes(bool isDescending)
        {
            var all = flavourCombinationRepository.All();

            ICollection<FlavourCombination> filteredData = null;

            if(isDescending)
            {
                filteredData = await all.OrderByDescending(fc => fc.Votes).ToListAsync();
            }
            else
            {
                filteredData = await all.OrderBy(fc => fc.Votes).ToListAsync();
            }

            return filteredData;
        }

        public async Task<FlavourCombination> GetFlavourCombinationByName(string name)
        {
            return await flavourCombinationRepository.All().Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<ICollection<FlavourCombination>> GetFlavourCombinationsContaining(int flavourId)
        {
            var data = await flavourCombinationRepository
                .All()
                .Where(fc => fc.FlavourCombinationReferences.Select(f => f.FlavourId).Contains(flavourId))
                .ToListAsync();

            return data;
        }

        public async Task<ICollection<FlavourCombination>> GetTop10Combinations()
        {
            return await flavourCombinationRepository
                .All()
                .OrderByDescending(fc => fc.Votes)
                .Take(10)
                .ToListAsync();
                
        }

        public async Task<ResultStatus> UpdateFlavourCombination(int id, string newName)
        {
            FlavourCombination target = await GetFlavourCombinationById(id);
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

        public async Task<FlavourCombination> GetFlavourCombinationById(int id)
        {
            return await flavourCombinationRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
