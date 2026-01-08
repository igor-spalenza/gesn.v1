using FluentValidation;
using gesn.webApp.Models.ViewModels.Offer.CompositeProduct;

namespace gesn.webApp.Validators.Offer.CompositeProduct
{
    public class BasicOfferInsertValidator : AbstractValidator<BasicOfferInsertVM>
    {
        public BasicOfferInsertValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da oferta é obrigatório.")
                .MaximumLength(200).WithMessage("O nome da oferta não pode exceder 200 caracteres.")
                .Must(NameWithoutNumbers).WithMessage("O nome da oferta não pode conter números!");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("O preço da oferta é obrigatório.")
                .GreaterThanOrEqualTo(0).WithMessage("O preço da oferta deve ser maior ou igual a zero.");
        }

        private bool NameWithoutNumbers(string name)
        {
            bool flagWithoutNumbers = true;
            foreach (char c in name)
                if (char.IsDigit(c))
                    flagWithoutNumbers = false;

            return flagWithoutNumbers;
        }
    }
}
