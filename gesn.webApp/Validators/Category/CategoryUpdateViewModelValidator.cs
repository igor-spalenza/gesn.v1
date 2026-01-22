using FluentValidation;
using gesn.webApp.Models.ViewModels.Global;

namespace gesn.webApp.Validators.Category
{
    public class CategoryUpdateViewModelValidator : AbstractValidator<CategoryUpdateViewModel>
    {
        public CategoryUpdateViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da categoria não pode exceder 100 caracteres.");

            RuleFor(RuleFor => RuleFor.Id)
                .NotEmpty().WithMessage("O ID da categoria é obrigatório.");

            //RuleFor(x => x.LastModifiedBy)
            //    .NotEmpty().WithMessage("O campo 'Última modificação por' é obrigatório.")
            //    .MaximumLength(100).WithMessage("O campo 'Última modificação por' não pode exceder 100 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("O campo 'Descrição' não pode exceder 500 caracteres.");

            RuleFor(x => x.LastModifiedAt)
                .Must(CheckDateTimeNotInFuture).WithMessage("O campo 'Última modificação' não pode conter uma data futura.");
        }

        private bool CheckDateTimeNotInFuture(DateTime dateTime) =>
         dateTime <= DateTime.Now;
    }
}
