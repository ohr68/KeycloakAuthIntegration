using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KeycloakAuthIntegration.Keycloak.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureKeycloakIntegration(this IServiceCollection services,
        IConfiguration configuration)
    {
        
        
        return services;
    }

    private static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {
        
        
        return services;
    }
}