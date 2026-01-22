using FluentValidation;
using gesn.webApp.Validators.Category;
using gesn.webApp.Validators.Type;

namespace gesn.webApp.Infrastructure.FluentValidation
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CategoryInsertViewModelValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryUpdateViewModelValidator>();
            services.AddValidatorsFromAssemblyContaining<TypeInsertViewModelValidator>();
            services.AddValidatorsFromAssemblyContaining<TypeUpdateViewModelValidator>();

            return services;
        }
    }
}