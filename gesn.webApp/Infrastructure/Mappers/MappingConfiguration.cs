using Mapster;
using MapsterMapper;
using System.Reflection;

namespace gesn.webApp.Infrastructure.Mappers
{
    public static class MappingConfiguration
    {
        public static IServiceCollection RegisterMaps(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}
