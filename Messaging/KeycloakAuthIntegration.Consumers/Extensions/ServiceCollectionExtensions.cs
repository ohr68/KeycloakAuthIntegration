using System.Reflection;
using System.Text.Json;
using KeycloakAuthIntegration.Common.Messaging.Interfaces;
using KeycloakAuthIntegration.Common.Messaging.Services;
using KeycloakAuthIntegration.Consumers.Consumers;
using KeycloakAuthIntegration.Keycloak;
using KeycloakAuthIntegration.Keycloak.Configuration;
using KeycloakAuthIntegration.Keycloak.Interfaces;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.RequestHandlers;
using KeycloakAuthIntegration.Keycloak.Requests;
using KeycloakAuthIntegration.Keycloak.Saga.CreateUser;
using KeycloakAuthIntegration.Keycloak.Saga.Interfaces;
using KeycloakAuthIntegration.Keycloak.Saga.Interfaces.CreateUser;
using KeycloakAuthIntegration.Keycloak.Services;
using KeycloakAuthIntegration.Messaging.Application.Extensions;
using KeycloakAuthIntegration.Messaging.Models;
using KeycloakAuthIntegration.Messaging.ORM.Extensions;
using MassTransit;
using Microsoft.Identity.Client;
using Refit;

namespace KeycloakAuthIntegration.Consumers.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services
            .AddApplicationLayer()
            .AddPersistenceLayer(configuration, isDevelopment)
            .AddMessaging(configuration)
            .ConfigureKeycloakIntegration(configuration);

        return services;
    }

    private static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MassTransitSettings>(configuration.GetSection("MasstransitSettings"));

        services.AddMassTransit(busConfig =>
        {
            busConfig.AddConsumer<UserCreatedConsumer>();

            busConfig.UsingRabbitMq((context, config) =>
            {
                var massTansitSettings = configuration.GetSection("MasstransitSettings").Get<MassTransitSettings>()
                                         ?? throw new InvalidOperationException(
                                             $"A chave {nameof(MassTransitSettings)} não foi encontrada ou não foi configurada corretamente.");

                config.Host(massTansitSettings.Host!, "/", x =>
                {
                    x.Username(massTansitSettings.User!);
                    x.Password(massTansitSettings.Password!);
                });

                config.ConfigureEndpoints(context);
            });
        });
        
        services.AddScoped<IQueueService, QueueService>();

        return services;
    }
    
    // TODO: refatorar
    private static IServiceCollection ConfigureKeycloakIntegration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<KeycloakUserOptions>(configuration.GetSection("KeycloakUser"));
        
        services.AddTransient<CustomRequestHandler>();
        services.AddTransient<ConsumerAuthHeaderHandler>();

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
                .AddHttpMessageHandler<CustomRequestHandler>()
                ;

            _ = refitInterface == typeof(IAuthRequests) 
                ? refitClient 
                : refitClient.AddHttpMessageHandler<ConsumerAuthHeaderHandler>();
        }

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthRequestHandler, AuthRequestHandler>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IClientRoleService, ClientRoleService>();
        services.AddScoped<IRealmRoleService, RealmRoleService>();
        services.AddScoped<IRealmHandler, RealmHandler>();
        services.AddScoped<IKeycloakClientHandler, KeycloakClientHandler>();
        services.AddScoped<ICreateUserHandler, CreateUserHandler>();
        services.AddScoped<ICreateUserSaga, CreateUserSaga>();
        
        return services;
    }
}