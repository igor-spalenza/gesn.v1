using FluentValidation;
using gesn.webApp.Models.ViewModels.Global;

namespace gesn.webApp.Validators.Type
{
    public class TypeUpdateViewModelValidator : AbstractValidator<TypeUpdateViewModel>
    {
        public TypeUpdateViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do tipo é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do tipo não pode exceder 100 caracteres.");

            RuleFor(RuleFor => RuleFor.Id)
                .NotEmpty().WithMessage("O ID do tipo é obrigatório.");

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
