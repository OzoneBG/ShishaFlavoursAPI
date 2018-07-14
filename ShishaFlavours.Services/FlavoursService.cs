namespace ShishaFlavoursAPI.Services
{
    using Microsoft.EntityFrameworkCore;
    using ShishaFlavoursAPI.Data.Common.Repository;
    using ShishaFlavoursAPI.Models;
    using ShishaFlavoursAPI.Services.Interfaces;
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

        public async Task CreateFlavour(string name)
        {
            Flavour newFlavour = new Flavour() { Name = name };

            flavoursRepository.Add(newFlavour);
            await flavoursRepository.SaveChanges();
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
    }
}
