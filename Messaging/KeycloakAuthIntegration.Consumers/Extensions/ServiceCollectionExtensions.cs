using KeycloakAuthIntegration.Consumers.Consumers;
using KeycloakAuthIntegration.Messaging.Models;
using KeycloakAuthIntegration.Messaging.ORM.Extensions;
using MassTransit;

namespace KeycloakAuthIntegration.Consumers.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services
            .AddPersistenceLayer(configuration, isDevelopment)
            .AddMessaging(configuration);

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

        return services;
    }
}