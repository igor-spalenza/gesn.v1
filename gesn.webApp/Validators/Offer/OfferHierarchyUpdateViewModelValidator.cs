using FluentValidation;
using gesn.webApp.Models.ViewModels.Offer;

namespace gesn.webApp.Validators.Offer
{
    public class OfferHierarchyUpdateViewModelValidator : AbstractValidator<OfferHierarchyUpdateViewModel>
    {
        public OfferHierarchyUpdateViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da hierarquia é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da hierarquia não pode exceder 100 caracteres.");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID da hierarquia é obrigatório.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("O campo 'Descrição' não pode exceder 500 caracteres.");

            RuleFor(x => x.LastModifiedAt)
                .Must(CheckDateTimeNotInFuture).WithMessage("O campo 'Última modificação' não pode conter uma data futura.");
        }

        private bool CheckDateTimeNotInFuture(DateTime dateTime) =>
            dateTime <= DateTime.Now;
    }
}
