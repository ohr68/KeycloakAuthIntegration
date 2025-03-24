using KeycloakAuthIntegration.Messaging.Interfaces;
using KeycloakAuthIntegration.Messaging.Models;
using KeycloakAuthIntegration.Messaging.Services;
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