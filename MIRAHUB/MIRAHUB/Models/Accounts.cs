using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace MIRAHUB.Models
{
    public class Accounts
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAccount { get; set; }
        public string Password { get; set; }
    }
}
