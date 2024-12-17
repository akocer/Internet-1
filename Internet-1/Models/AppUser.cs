using Microsoft.AspNetCore.Identity;

namespace Internet_1.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
