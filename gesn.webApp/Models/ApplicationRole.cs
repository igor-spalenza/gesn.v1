using Microsoft.AspNetCore.Identity;

namespace gesn.webApp.Models
{
    public class ApplicationRole : IdentityRole
    {
        public DateTime? CreatedDate { get; set; }
    }
}