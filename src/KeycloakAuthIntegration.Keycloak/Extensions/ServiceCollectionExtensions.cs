using System.Reflection;
using KeycloakAuthIntegration.Keycloak.Configuration;
using KeycloakAuthIntegration.Keycloak.Interfaces;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.RequestHandlers;
using KeycloakAuthIntegration.Keycloak.Requests;
using KeycloakAuthIntegration.Keycloak.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace KeycloakAuthIntegration.Keycloak.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureKeycloakIntegration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<KeycloakUserOptions>(configuration.GetSection("KeycloakUser"));

        services.AddTransient<CustomRequestHandler>();
        services.AddTransient<AuthHeaderHandler>();

        services
            .AddClients(configuration)
            .AddServices();
        
        return services;
    }

    private static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration.GetSection("Keycloak:auth-server-url").Value
                      ?? throw new InvalidOperationException("Chave 'Keycloak:auth-server-url' não encontrada.");

        var baseInterface = typeof(IRequest);

        var refitInterfaces = Assembly.GetAssembly(typeof(KeycloakLayer))!
            .GetTypes()
            .Where(t => t.IsInterface && baseInterface.IsAssignableFrom(t) && t != baseInterface)
            .ToList();

        foreach (var refitInterface in refitInterfaces)
        {
            var refitClient = services
                .AddRefitClient(refitInterface)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl))
                .AddHttpMessageHandler<CustomRequestHandler>();

            _ = refitInterface == typeof(IAuthRequests) 
                ? refitClient 
                : refitClient.AddHttpMessageHandler<AuthHeaderHandler>();
        }

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthRequestHandler, AuthRequestHandler>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRealmHandler, RealmHandler>();
        services.AddScoped<IKeycloakClientHandler, KeycloakClientHandler>();
        
        return services;
    }
}