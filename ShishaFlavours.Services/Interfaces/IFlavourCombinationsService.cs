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

        Task<ResultStatus> DeleteFlavourCombination(int id);

        Task<ResultStatus> UpdateFlavourCombination(int id, string newName);

        Task<ICollection<FlavourCombination>> GetFlavourCombinationsContaining(int flavourId);

        Task<ICollection<FlavourCombination>> GetTop10Combinations();

        Task<ICollection<FlavourCombination>> GetCombinationsByVotes(bool isDescending);

        Task<FlavourCombination> GetFlavourCombinationById(int id);
    }
}
