using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            // Register application services here
            // Example: services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
