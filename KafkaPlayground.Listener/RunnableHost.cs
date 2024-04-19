using KafkaPlayground.Listener.Consumers;
using Microsoft.Extensions.Hosting;

namespace KafkaPlayground.Listener
{
    internal class RunnableHost : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<IConsumer> _consumers;

        public RunnableHost(IServiceProvider serviceProvider, IEnumerable<IConsumer> consumers)
        {
            _serviceProvider = serviceProvider;
            _consumers = consumers;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var consumer in _consumers)
            {
                Task.Run(() => consumer.StartConsume());
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var consumer in _consumers)
            {
                consumer.StopConsume();
            }
        }
    }
}