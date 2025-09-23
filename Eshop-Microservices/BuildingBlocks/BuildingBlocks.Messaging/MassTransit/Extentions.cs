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
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    var username = configuration["MessageBroker:UserName"] 
                        ?? throw new ArgumentNullException("MessageBroker:UserName", "RabbitMQ username configuration is missing.");
                    var password = configuration["MessageBroker:Password"] 
                        ?? throw new ArgumentNullException("MessageBroker:Password", "RabbitMQ password configuration is missing.");
                    host.Username(username);
                    host.Password(password);
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}