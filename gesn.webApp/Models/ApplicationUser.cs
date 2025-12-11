using Microsoft.AspNetCore.Identity;

namespace gesn.webApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        public DateTime? CreatedDate { get; set; }
    }
}
