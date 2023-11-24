using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UHP.Application.Configurations;
using UHP.Infrastructure.Configurations;
using UHP.Persistence.Configurations;

namespace UHP.WebApi.Configurations
{

    public static class ConfigurationOptions
    {
        public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistenceSettings(configuration);
            services.AddApplicationSettings(configuration);
            services.AddInfrastructureSettings(configuration);
        }
    }
}