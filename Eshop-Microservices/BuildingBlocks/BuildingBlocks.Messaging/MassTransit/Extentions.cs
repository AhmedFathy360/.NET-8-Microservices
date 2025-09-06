using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTransit;
public static class Extentions
{
    public static IServiceCollection AddMessageBroker
        (this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                var hostUri = configuration["MessageBroker:Host"];
                var username = configuration["MessageBroker:UserName"];
                var password = configuration["MessageBroker:Password"];

                if (string.IsNullOrWhiteSpace(hostUri))
                    throw new ArgumentNullException("MessageBroker:Host", "RabbitMQ host URI is not configured.");
                if (string.IsNullOrWhiteSpace(username))
                    throw new ArgumentNullException("MessageBroker:UserName", "RabbitMQ username is not configured.");
                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentNullException("MessageBroker:Password", "RabbitMQ password is not configured.");

                configurator.Host(new Uri(hostUri), host =>
                {
                    host.Username(username);
                    host.Password(password);
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}