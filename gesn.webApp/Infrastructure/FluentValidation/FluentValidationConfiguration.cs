using FluentValidation;
using gesn.webApp.Validators.Category;
using gesn.webApp.Validators.Offer;
using gesn.webApp.Validators.Type;

namespace gesn.webApp.Infrastructure.FluentValidation
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            #region Category Validators
            services.AddValidatorsFromAssemblyContaining<CategoryInsertViewModelValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryUpdateViewModelValidator>();
            #endregion

            #region Type Validators
            services.AddValidatorsFromAssemblyContaining<TypeInsertViewModelValidator>();
            services.AddValidatorsFromAssemblyContaining<TypeUpdateViewModelValidator>();
            #endregion

            #region Offer Validators
            services.AddValidatorsFromAssemblyContaining<OfferUpdateViewModelValidator>();
            #endregion

            #region OfferHierarchy Validators
            services.AddValidatorsFromAssemblyContaining<OfferHierarchyInsertViewModelValidator>();
            services.AddValidatorsFromAssemblyContaining<OfferHierarchyUpdateViewModelValidator>();
            #endregion
            return services;
        }
    }
}