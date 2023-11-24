using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UHP.Infrastructure.Configurations.Settings;
using UHP.Infrastructure.Interfaces;
using UHP.Infrastructure.Service;

namespace UHP.Infrastructure.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var emailConfiguration = new EmailConfiguration();
            configuration.GetSection(nameof(EmailConfiguration)).Bind(emailConfiguration);
            services.AddSingleton(emailConfiguration);
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IQrCodeService, QrCodeService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IFileSystemService, FileSystemService>();
        }
    }
}