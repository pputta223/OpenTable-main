using System.ComponentModel.DataAnnotations;

namespace OpenTable.Models.DomainModels
{
    public class Metropolis
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
