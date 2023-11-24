using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UHP.Application.Configurations;
using UHP.Infrastructure.Configurations;
using UHP.Persistence.Configurations;

namespace UHP.WebApi.Configurations
{
    public static class FrameworkConfiguration
    {
        public static void AddFrameworkServices(this IServiceCollection services)
        {
            services.AddPersistenceServices();
            services.AddApplicationServices();
            services.AddInfrastructureServices();
            services.AddSwaggerServices();
        }
        
        public static void UseFrameworkServices(this IApplicationBuilder app)
        {
            app.UseSwaggerServices();
        }
    }
}