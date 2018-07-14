namespace ShishaFlavoursAPI.Services.Interfaces
{
    using ShishaFlavoursAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFlavoursService
    {
        Task CreateFlavour(string name);

        Task<Flavour> GetFlavourByName(string name);

        Task<ICollection<Flavour>> GetAllFlavours();

        Task AddFlavoursBulkAsync(Flavour[] flavours);
    }
}
