using gesn.webApp.Areas.Admin.Models.User;

namespace gesn.webApp.Areas.Admin.Models.Claim
{
   public class ClaimDetailViewModel
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public List<UserSelectionViewModel> UsersWithClaim { get; set; } = new();
        public List<RoleSelectionClaimViewModel> RolesWithClaim { get; set; } = new();
        public int TotalUsers { get; set; }
        public int TotalRoles { get; set; }

        // Propriedades de compatibilidade
        public int UserCount => TotalUsers;
        public int RoleCount => TotalRoles;
    }
}