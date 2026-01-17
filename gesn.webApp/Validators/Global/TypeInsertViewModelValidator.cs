using FluentValidation;
using gesn.webApp.Models.ViewModels.Global;

namespace gesn.webApp.Validators.Type
{
    public class TypeInsertViewModelValidator : AbstractValidator<TypeInsertViewModel>
    {
        public TypeInsertViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do tipo é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do tipo não pode exceder 100 caracteres.");
        }
    }
}
