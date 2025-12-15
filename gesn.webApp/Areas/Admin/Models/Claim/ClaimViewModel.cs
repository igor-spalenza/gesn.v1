using gesn.webApp.Areas.Admin.Models.User;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Areas.Admin.Models.Claim
{
    public class ClaimViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "O tipo da claim é obrigatório")]
        [Display(Name = "Tipo")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor da claim é obrigatório")]
        [Display(Name = "Valor")]
        public string Value { get; set; } = string.Empty;

        [Display(Name = "Usuários")]
        public string? Users { get; set; }

        [Display(Name = "Roles")]
        public string? Roles { get; set; }

        [Display(Name = "Quantidade de Usuários")]
        public int UserCount { get; set; }

        [Display(Name = "Quantidade de Roles")]
        public int RoleCount { get; set; }

        // Propriedades adicionais para compatibilidade com views
        public List<UserSelectionViewModel> UsersWithClaim { get; set; } = new();
        public List<RoleSelectionClaimViewModel> RolesWithClaim { get; set; } = new();
    }
}