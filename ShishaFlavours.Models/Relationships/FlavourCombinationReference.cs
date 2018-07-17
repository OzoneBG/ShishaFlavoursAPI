using ShishaFlavoursAPI.Models;

namespace ShishaFlavours.Models.Relationships
{
    public class FlavourCombinationReference
    {
        public int FlavourId { get; set; }
        public Flavour Flavour { get; set; }

        public int FlavourCombinationId { get; set; }
        public FlavourCombination FlavourCombination { get; set; }
    }
}
