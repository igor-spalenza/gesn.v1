using gesn.webApp.Areas.Admin.Models.Claim;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Areas.Admin.Models.User
{
    public class UserViewModel
    {
        public string? Id { get; set; }

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
        public string? Roles { get; set; }

        [Display(Name = "Claims")]
        public List<ClaimViewModel> Claims { get; set; } = new();
    }
}