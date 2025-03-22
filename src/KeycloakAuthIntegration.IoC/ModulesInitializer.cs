using KeycloakAuthIntegration.Application.Extensions;
using KeycloakAuthIntegration.Keycloak.Extensions;
using KeycloakAuthIntegration.ORM.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KeycloakAuthIntegration.IoC;

public static class ModulesInitializer
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration,
        bool isDevelopment = false)
    {
        services
            .AddApplicationLayer()
            .AddPersistenceLayer(configuration, isDevelopment)
            .ConfigureKeycloakIntegration(configuration);

        return services;
    }
}