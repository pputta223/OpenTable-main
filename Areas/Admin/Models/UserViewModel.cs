using Microsoft.AspNetCore.Identity;
using OpenTable.Models.DomainModels;

namespace OpenTable.Areas.Admin.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; } = null!;
        public IEnumerable<IdentityRole> Roles { get; set; } = null!;
    }
}
