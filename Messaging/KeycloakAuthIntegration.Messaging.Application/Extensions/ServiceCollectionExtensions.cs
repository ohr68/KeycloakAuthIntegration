using FluentValidation;
using KeycloakAuthIntegration.Keycloak;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace KeycloakAuthIntegration.Messaging.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddValidation()
            .ConfigureMapster();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(new[]
                { typeof(ApplicationLayer).Assembly, typeof(KeycloakLayer).Assembly }));

        return services;
    }

    private static IServiceCollection ConfigureMapster(this IServiceCollection services)
    {
        services.AddMapster();

        TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies());
        TypeAdapterConfig.GlobalSettings.AllowImplicitSourceInheritance = true;
        TypeAdapterConfig.GlobalSettings.AllowImplicitDestinationInheritance = true;

        return services;
    }

    private static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationLayer).Assembly);
        return services;
    }
}