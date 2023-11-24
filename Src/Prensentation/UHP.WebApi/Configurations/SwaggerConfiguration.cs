using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.AspNetCore;
using SchemaType = NJsonSchema.SchemaType;

namespace UHP.WebApi.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerDocument(c =>
            {
                c.SerializerSettings = new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver(),
                    Formatting = Formatting.Indented
                };
            });
        }

        public static void UseSwaggerServices(this IApplicationBuilder app)
        {
            app.UseOpenApi(c =>
            {
                c.PostProcess = (document, context) =>
                {
                    document.SchemaType = SchemaType.Swagger2;
                    document.Info.Version = "v1.0";
                    document.Info.Title = "UHP";
                    document.Info.Description = "Dotnet core UHP Api";
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "Jean Paul & Serge",
                    };
                    document.Host = "http://localhost:5000";
                    document.SecurityDefinitions.Add("JWT Token", new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "Bearer + token",
                        In = OpenApiSecurityApiKeyLocation.Header
                    });
                    document.Security.Add(new OpenApiSecurityRequirement() {{"JWT Token", new List<string>() { }}});
                    document.Schemes.Add(OpenApiSchema.Https);
                };
            });

            app.UseSwaggerUi3(c =>
            {
                c.SwaggerRoutes.Add(new SwaggerUi3Route("UHP Api v1.0", "/swagger/v1/swagger.json"));
                c.DocExpansion = "none";
            });
        }
    }
}