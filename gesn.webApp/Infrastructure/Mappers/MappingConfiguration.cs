using Mapster;

namespace gesn.webApp.Infrastructure.Mappers
{
    public static class MappingConfiguration
    {
        public static IServiceCollection RegisterMaps(this IServiceCollection services)
        {
            services.AddMapster();
            return services;
        }
    }
}
