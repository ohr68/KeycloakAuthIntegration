using System.Reflection;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using KeycloakAuthIntegration.WebApi.Constants;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .ConfigureKeycloakAuth(configuration)
            .AddSwagger(configuration)
            .ConfigureCors(configuration);
        
        return services;
    }

    private static IServiceCollection ConfigureKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .ConfigureKeycloakAuth(configuration)
            .ConfigureKeycloakAuthorization(configuration);
        
        return services;
    }

    private static IServiceCollection ConfigureKeycloakAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddKeycloakWebApiAuthentication(configuration);
        
        return services;
    }

    private static IServiceCollection ConfigureKeycloakAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddKeycloakAuthorization(configuration);
        
        return services;
    }
    
    private static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Auth Web Web API",
                Description = ""
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
        
        return services;
    }

    private static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Configuration.AllowProductManagementClient, policy =>
            {
                policy.WithOrigins(configuration.GetSection("AllowedClients:ProductManagement").Value!)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        return services;
    }
}