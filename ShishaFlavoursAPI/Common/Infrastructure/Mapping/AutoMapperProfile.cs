namespace ShishaFlavoursAPI.Common.Infrastructure.Mapping
{
    using AutoMapper;
    using ShishaFlavoursAPI.Common.Infrastructure.Mapping.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            var allTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains("ShishaFlavoursAPI"))
                .SelectMany(a => a.GetTypes());

            ConfigureMappings(allTypes);
            ConfigureCustomMappings(allTypes);
        }

        private void ConfigureMappings(IEnumerable<Type> allTypes)
        {
            allTypes
                .Where(t => t.IsClass && !t.IsAbstract && t
                    .GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Select(i => i.GetGenericTypeDefinition())
                    .Contains(typeof(IMapFrom<>)))
               .Select(t => new
               {
                   Destination = t,
                   Source = t
                        .GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => new
                        {
                            Definition = i.GetGenericTypeDefinition(),
                            Arguments = i.GetGenericArguments()
                        })
                        .Where(i => i.Definition == typeof(IMapFrom<>))
                        .SelectMany(i => i.Arguments)
                        .First(),
               })
               .ToList()
               .ForEach(mapping => CreateMap(mapping.Source, mapping.Destination));
        }

        private void ConfigureCustomMappings(IEnumerable<Type> allTypes)
        {
            allTypes
                .Where(t => t.IsClass
                    && !t.IsAbstract
                    && typeof(IHaveCustomMappings).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMappings>()
                .ToList()
                .ForEach(mapping => mapping.ConfigureMapping(this));
        }
    }
}
}
