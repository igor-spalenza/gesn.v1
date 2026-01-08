using FluentValidation;
using gesn.webApp.Models.ViewModels.Offer.CompositeProduct;
using gesn.webApp.Validators.Offer.CompositeProduct;

namespace gesn.webApp.Infrastructure.FluentValidation
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<BasicOfferInsertVM>, BasicOfferInsertValidator>();

            return services;
        }
    }
}
