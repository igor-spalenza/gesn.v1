using FluentValidation;
using gesn.webApp.Models.ViewModels.Offer;
using Microsoft.Extensions.Localization;

namespace gesn.webApp.Validators.Offer
{
    public class OfferInsertViewModelValidator : AbstractValidator<OfferInsertViewModel>
    {
        public OfferInsertViewModelValidator(IStringLocalizer<OfferInsertViewModel> localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da oferta é obrigatório.")
                .MaximumLength(200).WithMessage("O nome da oferta não pode exceder 200 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("A descrição da oferta não pode exceder 1000 caracteres.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("O preço é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("O preço da oferta deve ser maior ou igual a zero.");

            RuleFor(x => x.UnitPrice)
                .NotEmpty().WithMessage("O preço unitário é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("O preço unitário da oferta deve ser maior ou igual a zero.");

            RuleFor(x => x.AssemblyTime)
                .NotEmpty().WithMessage("O tempo de preparo é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("O tempo de preparo não pode ser negativo.");

            RuleFor(x => x.Cost)
                .NotEmpty().WithMessage("O custo é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage(localizer["Offer_Greater_Than_Or_Equal_To_Error_Message"]);
        }
    }
}
