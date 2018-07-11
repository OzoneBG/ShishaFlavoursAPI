namespace ShishaFlavoursAPI.Services
{
    using ShishaFlavoursAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFlavoursService
    {
        Task CreateFlavour(string name);

        void AddRangeOfFlavours(List<Flavour> flavours);

        Task<Flavour> GetFlavourByName(string name);
    }
}
