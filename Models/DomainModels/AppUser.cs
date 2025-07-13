using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OpenTable.Models.Validations; // for Remote attribute

namespace OpenTable.Models.DomainModels
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter full name.")]
        [StringLength(100, ErrorMessage = "Full name may not exceed 100 characters.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Username can only contain letters and numbers.")]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Please enter a Email Address.")]
        [Remote("CheckEmail", "Validation")]
        [EmailAddress]
        [Display(Name = "Email Address")]

        public string? Email { get; set; }


        [Required(ErrorMessage = "Please enter a date of birth.")]
        [MaximumAge(100, ErrorMessage = "You cannot be more than 100 years old.")]
        [IsNotFutureDate(ErrorMessage = "Your DOB must be past date.")]

        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [Phone]
        [RegularExpression(@"^\d{3}[-.]\d{3}[-.]\d{4}$", ErrorMessage = "Phone must be in the format 123-456-7890 or 123.456.7890.")]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public UserType UserType { get; set; } = UserType.Client;

    }
}
