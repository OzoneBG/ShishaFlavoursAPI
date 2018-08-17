namespace ShishaFlavours.Services.Interfaces
{
    using ShishaFlavours.Services.ResponseModels;
    using ShishaFlavoursAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFlavoursService
    {
        Task<ResultStatus> CreateFlavour(string name);

        Task<ResultStatus> DeleteFlavourById(int id);

        Task<ResultStatus> UpdateFlavourById(int id, string newName);

        Task<Flavour> GetFlavourByName(string name);

        Task<Flavour> GetFlavourById(int id);

        Task<ICollection<Flavour>> GetAllFlavours();

        Task AddFlavoursBulkAsync(Flavour[] flavours);
    }
}
