using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OpenTable.Models.DomainModels
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter restaurant name.")]
        [StringLength(150)]
        [Display(Name = "Restaurant Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please choose cuisine style.")]
        [Display(Name = "Cuisine Style")]
        public string CuisineId { get; set; } // Foreign key property
        [ValidateNever]
        public Cuisine Cuisine { get; set; } = null!;       // Navigation property


        [Required(ErrorMessage = "Please choose price range.")]
        [Display(Name = "Price Range")]
        public string PriceId { get; set; } // Foreign key property

        [ValidateNever]
        public Price Price { get; set; } = null!;       // Navigation property


        [Required(ErrorMessage = "Please choose open hours.")]
        [Display(Name = "Open Hours")]

        public string OpenHours { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please choose open hours.")]
        [Display(Name = "Address")]

        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please choose phone number.")]
        [Display(Name = "Phone")]
        [RegularExpression(@"^\d{3}[-.]\d{3}[-.]\d{4}$", ErrorMessage = "Phone must be in the format 123-456-7890 or 123.456.7890.")]

        public string? Phone { get; set; } = string.Empty;

        [Display(Name = "Metropolis")]
        public int MetropolisId { get; set; }

        [ForeignKey("MetropolisId")]
        public Metropolis Metropolis { get; set; } = null!;

        public string LogoPath { get; set; } = string.Empty;

        public string Slug => Name.Replace(' ', '-');

    }
}
