namespace ShishaFlavours.Services.Interfaces
{
    using ShishaFlavours.API.ResultModel;
    using ShishaFlavoursAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFlavourCombinationsService
    {
        Task CreateFlavourCombinationAsync(FlavourCombination flavourCombination, List<Flavour> flavours);

        Task<FlavourCombinationResultModel> GetFlavourCombinationByName(string name);
    }
}
