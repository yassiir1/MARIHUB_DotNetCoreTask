using MessagePack;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace MIRAHUB.Models
{
    public class Accounts
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAccount { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
    }
}
