using KafkaPlayground.Configuration;
using KafkaPlayground.Listener.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaPlayground.Listener
{
    public class Startup
    {
        internal static void ConfigureServices(IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddSingleton(config.GetSection(nameof(UserEventsConfiguration)).Get<UserEventsConfiguration>());
            serviceCollection.AddSingleton<IConsumer, UserEventsConsumer>();
            serviceCollection.AddSingleton(config.GetSection(nameof(VehicleEventsConfiguration)).Get<VehicleEventsConfiguration>());
            serviceCollection.AddSingleton<IConsumer, VehicleEventsConsumer>();
        }
    }
}
