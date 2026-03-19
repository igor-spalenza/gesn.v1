using FluentValidation;
using gesn.webApp.Models.ViewModels.Offer;

namespace gesn.webApp.Validators.Offer
{
    public class OfferHierarchyInsertViewModelValidator : AbstractValidator<OfferHierarchyInsertViewModel>
    {
        public OfferHierarchyInsertViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da hierarquia é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da hierarquia não pode exceder 100 caracteres.");
        }
    }
}
