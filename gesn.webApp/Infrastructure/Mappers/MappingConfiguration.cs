using Mapster;
using MapsterMapper;
using System.Reflection;

namespace gesn.webApp.Infrastructure.Mappers
{
    public static class MappingConfiguration
    {
        public static IServiceCollection RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
