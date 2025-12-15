using gesn.webApp.Areas.Admin.Models.Claim;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Areas.Admin.Models.Role
{
    public class RoleViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "O nome da role é obrigatório")]
        [Display(Name = "Nome da Role")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Nome Normalizado")]
        public string? NormalizedName { get; set; }

        [Display(Name = "Usuários")]
        public string? Users { get; set; }

        [Display(Name = "Claims")]
        public List<ClaimViewModel> Claims { get; set; } = new();

        [Display(Name = "Quantidade de Usuários")]
        public int UserCount { get; set; }
    }
}