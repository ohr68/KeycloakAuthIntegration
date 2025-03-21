using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureKeycloak(this IServiceCollection services, IConfiguration configuration)
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
}