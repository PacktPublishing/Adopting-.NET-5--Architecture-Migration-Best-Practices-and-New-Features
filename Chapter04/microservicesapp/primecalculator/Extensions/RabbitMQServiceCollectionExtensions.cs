using Microsoft.Extensions.DependencyInjection;
using primecalculator.Messaging;
using System;

namespace primecalculator.Extensions
{
    /// <summary>
    /// Extension methods for setting up IMQClient services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class RabbitMQServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Rabbit Message Queuing services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Add(ServiceDescriptor.Singleton<IMQClient, RabbitMQClient>());
            services.Add(ServiceDescriptor.Singleton<IMessageQueueSender, RabbitMQueueSender>());

            return services;
        }
    }
}