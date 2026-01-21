using FluentValidation;
using gesn.webApp.Models.ViewModels.Offer.Category;

namespace gesn.webApp.Validators.Category
{
    public class CategoryInsertViewModelValidator : AbstractValidator<CategoryInsertViewModel>
    {
        public CategoryInsertViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da categoria não pode exceder 100 caracteres.");
        }
    }
}
