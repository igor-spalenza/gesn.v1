using gesn.webApp.Areas.Admin.Models.Claim;
using System.ComponentModel.DataAnnotations;

namespace gesn.webApp.Areas.Admin.Models.Role
{
    public class CreateRoleViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "O nome da role é obrigatório")]
        [StringLength(256, ErrorMessage = "O nome da role deve ter no máximo {1} caracteres")]
        [Display(Name = "Nome da Role")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Claims")]
        public List<ClaimViewModel> Claims { get; set; } = new();

        [Display(Name = "Claims Disponíveis")]
        public List<string> AvailableClaimTypes { get; set; } = new();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Regra de negócio: uma role deve ter pelo menos 1 claim
            var validClaims = Claims?.Where(c => !string.IsNullOrWhiteSpace(c.Type) && !string.IsNullOrWhiteSpace(c.Value)).ToList();

            if (validClaims == null || !validClaims.Any())
            {
                yield return new ValidationResult(
                    "Uma role deve ter pelo menos uma claim associada. Adicione pelo menos uma claim antes de salvar.",
                    new[] { nameof(Claims) });
            }
        }
    }
}
