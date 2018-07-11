namespace ShishaFlavoursAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Flavour
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 20 characters long")]
        public string Name { get; set; }
    }
}
