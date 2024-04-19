using KafkaPlayground.Configuration;
using KafkaPlayground.Listener.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaPlayground.Listener
{
    public class Startup
    {
        internal static void ConfigureServices
        (
            IServiceCollection serviceCollection,
            IConfiguration config
        )
        {
            serviceCollection.Configure<UserEventsConfiguration>(config.GetSection(nameof(UserEventsConfiguration)));
            serviceCollection.Configure<VehicleEventsConfiguration>(config.GetSection(nameof(VehicleEventsConfiguration)));
            serviceCollection.AddScoped<IConsumer, UserEventsConsumer>();
            serviceCollection.AddScoped<IConsumer, VehicleEventsConsumer>();
        }
    }
}
