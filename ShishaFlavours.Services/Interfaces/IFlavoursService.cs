namespace ShishaFlavours.Services.Interfaces
{
    using ShishaFlavours.Services.ResponseModels;
    using ShishaFlavoursAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFlavoursService
    {
        Task<ResultStatus> CreateFlavour(string name);

        Task<ResultStatus> DeleteFlavourByName(string name);

        Task<ResultStatus> UpdateFlavourByName(string name, string newName);

        Task<Flavour> GetFlavourByName(string name);

        Task<ICollection<Flavour>> GetAllFlavours();

        Task AddFlavoursBulkAsync(Flavour[] flavours);
    }
}
