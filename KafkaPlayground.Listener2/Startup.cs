using KafkaPlayground.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaPlayground.Listener2
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

            //serviceCollection.AddScoped<IEventsStorage, EventsStorage>();
            //services.AddScoped<IEventToProcessRepository, EventToProcessRepository>();
        }
    }
}
