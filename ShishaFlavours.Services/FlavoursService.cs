namespace ShishaFlavours.Services
{
    using Microsoft.EntityFrameworkCore;
    using ShishaFlavours.Services.Interfaces;
    using ShishaFlavours.Services.ResponseModels;
    using ShishaFlavoursAPI.Data.Common.Repository;
    using ShishaFlavoursAPI.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FlavoursService : IFlavoursService
    {
        private readonly IRepository<Flavour> flavoursRepository = null;

        public FlavoursService(IRepository<Flavour> flavoursRepository)
        {
            this.flavoursRepository = flavoursRepository;
        }

        public async Task<Flavour> GetFlavourByName(string name)
        {
            Flavour flavour = null;

            flavour = await flavoursRepository.All().Where(f => f.Name == name).FirstOrDefaultAsync();

            return flavour;
        }

        public async Task<ICollection<Flavour>> GetAllFlavours()
        {
            ICollection<Flavour> allFlavours = await flavoursRepository.All().ToListAsync();

            return allFlavours;
        }

        public async Task AddFlavoursBulkAsync(Flavour[] flavours)
        {
            foreach(Flavour flavour in flavours)
            {
                flavoursRepository.Add(flavour);
            }

            await flavoursRepository.SaveChanges();
        }

        public async Task<ResultStatus> CreateFlavour(string name)
        {
            Flavour newFlavour = new Flavour() { Name = name };

            flavoursRepository.Add(newFlavour);
            int code = await flavoursRepository.SaveChanges();

            bool status = (code != 0);

            ResultStatus resultStatus = new ResultStatus
            {
                Status = status
            };

            if(status)
            {
                resultStatus.Message = "Successfully created new flavour";
            }
            else
            {
                resultStatus.Message = "Failed to create new flavour";
            }

            return resultStatus;
        }

        public async Task<ResultStatus> DeleteFlavourByName(string name)
        {
            Flavour target = await GetFlavourByName(name);
            flavoursRepository.Delete(target);
            int code = await flavoursRepository.SaveChanges();

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

        public async Task<ResultStatus> UpdateFlavourByName(string name, string newName)
        {
            Flavour target = await GetFlavourByName(name);
            target.Name = newName;
            flavoursRepository.Update(target);
            int code = await flavoursRepository.SaveChanges();

            bool status = (code != 0);

            ResultStatus resultStatus = new ResultStatus
            {
                Status = status
            };

            if (status)
            {
                resultStatus.Message = "Successfully updated flavour";
            }
            else
            {
                resultStatus.Message = "Failed to update flavour";
            }

            return resultStatus;
        }
    }
}
