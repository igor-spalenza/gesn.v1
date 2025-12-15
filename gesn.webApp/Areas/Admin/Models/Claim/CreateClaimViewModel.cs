using gesn.webApp.Areas.Admin.Models.User;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Areas.Admin.Models.Claim
{
    public class CreateClaimViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "O tipo da claim é obrigatório")]
        [StringLength(256, ErrorMessage = "O tipo da claim deve ter no máximo {1} caracteres")]
        [Display(Name = "Tipo da Claim")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor da claim é obrigatório")]
        [StringLength(256, ErrorMessage = "O valor da claim deve ter no máximo {1} caracteres")]
        [Display(Name = "Valor da Claim")]
        public string Value { get; set; } = string.Empty;

        [Display(Name = "Usuários Selecionados")]
        public List<string>? SelectedUsers { get; set; }

        [Display(Name = "Usuários Disponíveis")]
        public List<UserSelectionViewModel> AvailableUsers { get; set; } = new();

        [Display(Name = "Roles Selecionadas")]
        public List<string>? SelectedRoles { get; set; }

        [Display(Name = "Roles Disponíveis")]
        public List<RoleSelectionClaimViewModel> AvailableRoles { get; set; } = new();

        // Propriedades adicionais para compatibilidade com views
        [Display(Name = "IDs de Usuários Selecionados")]
        public List<string> SelectedUserIds { get; set; } = new();

        [Display(Name = "IDs de Roles Selecionadas")]
        public List<string> SelectedRoleIds { get; set; } = new();

        [Display(Name = "Tipos Comuns de Claims")]
        public List<string> CommonClaimTypes { get; set; } = new()
        {
            "permission.users.read", "permission.users.create", "permission.users.update", "permission.users.delete",
            "permission.roles.read", "permission.roles.create", "permission.roles.update", "permission.roles.delete",
            "permission.claims.read", "permission.claims.create", "permission.claims.update", "permission.claims.delete",
            "permission.admin.access", "permission.reports.read", "permission.settings.manage",
            "Nome", "CPF", "RG", "DataNascimento", "Endereco", "Cidade", "Estado", "CEP",
            "Departamento", "Cargo", "DataAdmissao", "Matricula", "NivelAcesso"
        };

        [Display(Name = "Tipos Disponíveis de Claims")]
        public List<string> AvailableClaimTypes => CommonClaimTypes;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var hasUsers = SelectedUsers?.Any() == true;
            var hasRoles = SelectedRoles?.Any() == true;

            if (!hasUsers && !hasRoles)
            {
                yield return new ValidationResult(
                    "Selecione pelo menos um usuário ou uma role para atribuir a claim.",
                    new[] { nameof(SelectedUsers), nameof(SelectedRoles) });
            }
        }
    }
}