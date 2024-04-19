using Confluent.Kafka;
using KafkaPlayground.Configuration;
using Microsoft.Extensions.Options;

namespace KafkaPlayground.Listener.Consumers
{
    public class UserEventsConsumer : IConsumer
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly UserEventsConfiguration _kafkaConfiguration;

        public UserEventsConsumer(IOptions<UserEventsConfiguration> configuration)
        {
            _kafkaConfiguration = configuration.Value;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _kafkaConfiguration.Server,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = _kafkaConfiguration.Username,
                SaslPassword = _kafkaConfiguration.Password,
                GroupId = _kafkaConfiguration.ConsumerGroupId,
                EnableAutoCommit = false
            };

            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        }

        public void StartConsume()
        {
            _consumer.Subscribe(_kafkaConfiguration.Topic);
            Console.WriteLine($"Subscribed to {_kafkaConfiguration.Topic}");
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

        public void StopConsume()
        {
            _consumer.Close();
        }
    }
}