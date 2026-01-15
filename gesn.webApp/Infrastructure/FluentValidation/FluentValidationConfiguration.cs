using FluentValidation;
using gesn.webApp.Models.ViewModels.Offer.Category;
using gesn.webApp.Models.ViewModels.Offer.CompositeProduct;
using gesn.webApp.Validators.Category;
using gesn.webApp.Validators.Offer.CompositeProduct;

namespace gesn.webApp.Infrastructure.FluentValidation
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CategoryInsertViewModelValidator>();
            //services.AddScoped<IValidator<BasicOfferInsertVM>, BasicOfferInsertValidator>();
            //services.AddScoped<IValidator<CategoryInsertViewModel>, CategoryInsertViewModelValidator>();
            return services;
        }
    }
}
