using Microsoft.AspNetCore.Identity;
namespace MIRAHUB.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool isActive { get; set; }
    }
}
