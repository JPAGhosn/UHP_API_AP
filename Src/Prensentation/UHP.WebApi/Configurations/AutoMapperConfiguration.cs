using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace UHP.WebApi.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperServices(this IServiceCollection services)
        {
    
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                var loadedProfiles = RetrieveProfiles();
                foreach (var profile in loadedProfiles)
                {
                    cfg.AddProfile(profile);
                }
            }).CreateMapper());
        }

        private static List<Type> RetrieveProfiles()
        {
            var loadedProfiles = ExtractProfile(AppDomain.CurrentDomain.Load("UHP.Application"));
            return loadedProfiles;
        }

        private static List<Type> ExtractProfile(Assembly assembly)
        {
            var profiles = new List<Type>();

            var assemblyProfiles = assembly.ExportedTypes.Where(type => type.IsSubclassOf(typeof(Profile)));
            profiles.AddRange(assemblyProfiles);

            return profiles;
        }
    }
}