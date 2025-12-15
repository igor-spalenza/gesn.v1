using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Areas.Admin.Models.Home
{
    public class AdminHomeViewModel
    {
        [Display(Name = "Total de Usuários")]
        public int TotalUsers { get; set; }

        [Display(Name = "Total de Roles")]
        public int TotalRoles { get; set; }

        [Display(Name = "Total de Claims")]
        public int TotalClaims { get; set; }

        [Display(Name = "Admins Ativos")]
        public int ActiveAdmins { get; set; }

        [Display(Name = "Usuários Ativos (últimos 30 dias)")]
        public int ActiveUsers { get; set; }

        [Display(Name = "Usuários Cadastrados Hoje")]
        public int UsersToday { get; set; }

        [Display(Name = "Claims Únicas")]
        public int UniqueClaims { get; set; }

        [Display(Name = "Roles com Claims")]
        public int RolesWithClaims { get; set; }

        // Estatísticas adicionais para gráficos futuros
        public List<UserStatsViewModel> RecentUsers { get; set; } = new();
        public List<RoleStatsViewModel> RoleDistribution { get; set; } = new();
        public List<ClaimStatsViewModel> TopClaims { get; set; } = new();
    }
}