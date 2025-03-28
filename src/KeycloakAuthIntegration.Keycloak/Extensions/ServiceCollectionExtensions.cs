using System.Reflection;
using KeycloakAuthIntegration.Keycloak.Configuration;
using KeycloakAuthIntegration.Keycloak.Configuration.Resilience;
using KeycloakAuthIntegration.Keycloak.Interfaces;
using KeycloakAuthIntegration.Keycloak.Interfaces.Services;
using KeycloakAuthIntegration.Keycloak.RequestHandlers;
using KeycloakAuthIntegration.Keycloak.Requests;
using KeycloakAuthIntegration.Keycloak.Saga.CreateUser;
using KeycloakAuthIntegration.Keycloak.Saga.Interfaces.CreateUser;
using KeycloakAuthIntegration.Keycloak.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
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

    public static IServiceCollection ConfigureKeycloakConsumerIntegration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<KeycloakUserOptions>(configuration.GetSection("KeycloakUser"));

        services.AddTransient<CustomRequestHandler>();
        services.AddTransient<ConsumerAuthHeaderHandler>();

        services
            .AddClients(configuration, isApi: false)
            .AddServices();

        return services;
    }

    private static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration,
        bool isApi = true)
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
                : isApi
                    ? refitClient.AddHttpMessageHandler<AuthHeaderHandler>()
                    : refitClient.AddHttpMessageHandler<ConsumerAuthHeaderHandler>();

            refitClient.AddResilienceHandler($"keycloak-{refitInterface.Name}", pipelineBuilder =>
            {
                pipelineBuilder.AddTimeout(TimeoutConfig.GetTimeoutPolicy());
                pipelineBuilder.AddRetry(RetryConfig.GetRetryOptions());
                pipelineBuilder.AddFallback(FallbackConfig.GetFallbackOptions());
                pipelineBuilder.AddCircuitBreaker(CircuitBreakerConfig.GetCircuitBreakerOptions());
            });
        }

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthRequestHandler, AuthRequestHandler>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IClientRoleService, ClientRoleService>();
        services.AddScoped<IRealmRoleService, RealmRoleService>();
        services.AddScoped<IRealmHandler, RealmHandler>();
        services.AddScoped<IKeycloakClientHandler, KeycloakClientHandler>();
        services.AddScoped<ICreateUserHandler, CreateUserHandler>();
        services.AddScoped<ICreateUserSaga, CreateUserSaga>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}