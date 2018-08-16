using System.Collections.Generic;

namespace ShishaFlavours.API.RequestModels.FlavourCombination
{
    public class FlavourCombinationCreateModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> Flavours { get; set; }
    }
}
