using gesn.webApp.Areas.Admin.Models.Claim;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Areas.Admin.Models.User
{
    public class EditUserViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome de usuário é obrigatório")]
        [Display(Name = "Nome de Usuário")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Telefone inválido")]
        [Display(Name = "Telefone")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Funções")]
        public List<string> SelectedRoles { get; set; } = new();

        [Display(Name = "Funções Disponíveis")]
        public List<string> AvailableRoles { get; set; } = new();

        [Display(Name = "Claims")]
        public List<ClaimViewModel> Claims { get; set; } = new();

        [Display(Name = "Claims Disponíveis")]
        public List<string> AvailableClaimTypes { get; set; } = new()
        {
            "Nome", "CPF", "RG", "DataNascimento", "Endereco",
            "Cidade", "Estado", "CEP", "Departamento", "Cargo",
            "DataAdmissao", "Matricula", "NivelAcesso"
        };
    }
}
