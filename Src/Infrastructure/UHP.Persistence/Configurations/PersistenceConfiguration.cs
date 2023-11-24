using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UHP.Persistence.Configurations.Settings;

namespace UHP.Persistence.Configurations
{
    public static class PersistenceConfiguration
    {
        private static StorageSettings _storageSettings;
        public static void AddPersistenceSettings(this IServiceCollection services, IConfiguration configuration)
        {
            _storageSettings = new StorageSettings();
            configuration.GetSection(nameof(StorageSettings)).Bind(_storageSettings);
            services.AddSingleton(_storageSettings);
        }
        
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<UhpContext>(options => options.UseNpgsql(_storageSettings.DefaultConnection), ServiceLifetime.Transient);
        }
    }
}