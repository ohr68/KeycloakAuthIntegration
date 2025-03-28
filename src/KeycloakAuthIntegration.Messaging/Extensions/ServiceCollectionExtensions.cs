using KeycloakAuthIntegration.Common.Messaging.Interfaces;
using KeycloakAuthIntegration.Common.Messaging.Services;
using KeycloakAuthIntegration.Messaging.Consumers;
using KeycloakAuthIntegration.Messaging.Models;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KeycloakAuthIntegration.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MassTransitSettings>(configuration.GetSection("MasstransitSettings"));
        
        services.AddMassTransit(busConfig =>
        {
            busConfig.AddConsumer<UserSynchronizedConsumer>();
            
            busConfig.UsingRabbitMq((context, config) =>
            {
                var massTansitSettings = configuration.GetSection("MasstransitSettings").Get<MassTransitSettings>()
                                         ?? throw new InvalidOperationException($"A chave {nameof(MassTransitSettings)} não foi encontrada ou não foi configurada corretamente.");
                
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
}