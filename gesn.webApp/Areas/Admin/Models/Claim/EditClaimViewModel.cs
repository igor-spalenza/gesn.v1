using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using gesn.webApp.Areas.Admin.Models.User;

namespace gesn.webApp.Areas.Admin.Models.Claim 
{
    public class EditClaimViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo da claim é obrigatório")]
        [StringLength(256, ErrorMessage = "O tipo da claim deve ter no máximo {1} caracteres")]
        [Display(Name = "Tipo da Claim")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "O valor da claim é obrigatório")]
        [StringLength(256, ErrorMessage = "O valor da claim deve ter no máximo {1} caracteres")]
        [Display(Name = "Valor da Claim")]
        public string Value { get; set; } = string.Empty;

        [Display(Name = "Usuários Associados")]
        public List<UserSelectionViewModel> AssociatedUsers { get; set; } = new();

        [Display(Name = "Roles Associadas")]
        public List<RoleSelectionClaimViewModel> AssociatedRoles { get; set; } = new();

        // Propriedades adicionais para compatibilidade com views
        [Display(Name = "Usuários Disponíveis")]
        public List<UserSelectionViewModel> AvailableUsers { get; set; } = new();

        [Display(Name = "Roles Disponíveis")]
        public List<RoleSelectionClaimViewModel> AvailableRoles { get; set; } = new();

        [Display(Name = "IDs de Usuários Selecionados")]
        public List<string>? SelectedUserIds { get; set; }

        [Display(Name = "IDs de Roles Selecionadas")]
        public List<string>? SelectedRoleIds { get; set; }

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
    }
}