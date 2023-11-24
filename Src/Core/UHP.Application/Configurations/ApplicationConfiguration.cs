using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UHP.Application.Configurations.Settings;

namespace UHP.Application.Configurations
{
    public static class ApplicationConfiguration
    {
        private static AppSettings _appSettings;

        public static void AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            _appSettings = new AppSettings();
            configuration.GetSection(nameof(AppSettings)).Bind(_appSettings);
            services.AddSingleton(_appSettings);
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
        }
    }
}