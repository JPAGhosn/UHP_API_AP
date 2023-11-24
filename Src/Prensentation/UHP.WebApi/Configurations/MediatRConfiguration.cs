using System;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace UHP.WebApi.Configurations
{
    public static class MediatRConfiguration
    {
        public static void AddMediatRServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddMediatR(AppDomain.CurrentDomain.Load("UHP.Application"));
        }
    }
}