using FluentValidation;
using gesn.webApp.Validators.Category;

namespace gesn.webApp.Infrastructure.FluentValidation
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CategoryInsertViewModelValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryUpdateViewModelValidator>();
            
            return services;        }
    }
}