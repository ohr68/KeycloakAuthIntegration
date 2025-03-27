using FluentValidation;
using KeycloakAuthIntegration.Application.CustomValidators;
using KeycloakAuthIntegration.Domain.Interfaces;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace KeycloakAuthIntegration.Application.Extensions;

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
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationLayer).Assembly));
        services.AddScoped<IPasswordHasher, CustomPasswordHasher>();

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