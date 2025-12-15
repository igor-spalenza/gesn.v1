using gesn.webApp.Areas.Admin.Models.User;
using gesn.webApp.Areas.Admin.Models.Claim;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Areas.Admin.Models.Role
{
    public class EditRoleViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome da role é obrigatório")]
        [StringLength(256, ErrorMessage = "O nome da role deve ter no máximo {1} caracteres")]
        [Display(Name = "Nome da Role")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Nome Normalizado")]
        public string? NormalizedName { get; set; }

        [Display(Name = "Claims")]
        public List<ClaimViewModel> Claims { get; set; } = new();

        [Display(Name = "Claims Disponíveis")]
        public List<string> AvailableClaimTypes { get; set; } = new();

        [Display(Name = "Usuários Associados")]
        public List<UserSelectionViewModel> AssociatedUsers { get; set; } = new();
    }

}
