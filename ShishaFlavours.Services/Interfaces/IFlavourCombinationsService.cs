namespace ShishaFlavours.Services.Interfaces
{
    using ShishaFlavours.Services.ResponseModels;
    using ShishaFlavoursAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFlavourCombinationsService
    {
        Task<ResultStatus> CreateFlavourCombinationAsync(FlavourCombination flavourCombination, List<Flavour> flavours);

        Task<FlavourCombination> GetFlavourCombinationByName(string name);

        Task<ICollection<FlavourCombination>> GetAllCombinations();

        Task<ResultStatus> DeleteFlavourCombination(string name);

        Task<ResultStatus> UpdateFlavourCombination(string name, string newName);
    }
}
