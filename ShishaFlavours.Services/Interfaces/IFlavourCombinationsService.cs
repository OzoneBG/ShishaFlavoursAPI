namespace ShishaFlavours.Services.Interfaces
{
    using ShishaFlavours.API.ResultModel;
    using ShishaFlavours.Services.ResponseModels;
    using ShishaFlavoursAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFlavourCombinationsService
    {
        Task<ResultStatus> CreateFlavourCombinationAsync(FlavourCombination flavourCombination, List<Flavour> flavours);

        Task<FlavourCombinationResultModel> GetFlavourCombinationByName(string name);
    }
}
