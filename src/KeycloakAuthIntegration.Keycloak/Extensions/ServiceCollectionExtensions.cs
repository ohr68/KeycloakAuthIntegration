using System.Reflection;
using KeycloakAuthIntegration.Keycloak.Interfaces;
using KeycloakAuthIntegration.Keycloak.MessageHandlers;
using KeycloakAuthIntegration.Keycloak.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace KeycloakAuthIntegration.Keycloak.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureKeycloakIntegration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddClients(configuration);
        
        return services;
    }

    private static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration.GetSection("KeycloakServer:Url").Value
                      ?? throw new InvalidOperationException("Chave 'KeycloakServer:Url' não encontrada.");

        var baseInterface = typeof(IRequest);

        var refitInterfaces = Assembly.GetAssembly(typeof(KeycloakLayer))!
            .GetTypes()
            .Where(t => t.IsInterface && baseInterface.IsAssignableFrom(t) && t != baseInterface)
            .ToList();

        foreach (var refitInterface in refitInterfaces)
        {
            var refitClient = services
                .AddRefitClient(refitInterface)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

            _ = refitInterface.DeclaringType == typeof(IAuthRequests) 
                ? refitClient 
                : refitClient.AddHttpMessageHandler<AuthHeaderHandler>();
        }

        return services;
    }
}