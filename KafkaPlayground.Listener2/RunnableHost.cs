using Confluent.Kafka;
using KafkaPlayground.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace KafkaPlayground.Listener2
{
    internal class RunnableHost : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConsumer<string, string> _consumer;
        private readonly UserEventsConfiguration _configuration;

        public RunnableHost(IServiceProvider serviceProvider, IOptions<UserEventsConfiguration> configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration.Value;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _configuration.Server,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = _configuration.Username,
                SaslPassword = _configuration.Password,
                GroupId = _configuration.ConsumerGroupId,
                EnableAutoCommit = false
            };

            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_configuration.Topic);
            while (true)
            {
                try
                {
                    var consumeResult = _consumer.Consume();
                    Console.WriteLine($"Received message: {consumeResult.Message.Value}");

                    // Process the message here
                    _consumer.Commit(consumeResult);
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error consuming message: {e.Error.Reason}");
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
        }
    }
}