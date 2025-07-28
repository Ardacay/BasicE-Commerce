using Microsoft.AspNetCore.Identity;

namespace ECommerceAuth.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

      
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; } 
        public string? ProfilePictureUrl { get; set; } 

       
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public bool? IsActive { get; set; } = true;


        public string? FullName => $"{FirstName} {LastName}";
    }
}
