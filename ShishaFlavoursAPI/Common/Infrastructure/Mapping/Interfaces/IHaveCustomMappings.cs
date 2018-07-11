namespace ShishaFlavoursAPI.Common.Infrastructure.Mapping.Interfaces
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void ConfigureMapping(Profile profile);
    }
}
