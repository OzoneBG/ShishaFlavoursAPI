namespace ShishaFlavoursAPI.Models
{
    using ShishaFlavours.Models.Relationships;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class FlavourCombination
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The length of the name must be between 3 and 30")]
        public string Name { get; set; }

        [StringLength(100, MinimumLength = 10, ErrorMessage = "The length of the description must be between 10 and 100")]
        public string Description { get; set; }

        public List<FlavourCombinationReference> FlavourCombinationReferences { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public User User { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
