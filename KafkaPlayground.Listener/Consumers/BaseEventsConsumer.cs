using Confluent.Kafka;
using KafkaPlayground.Configuration;

namespace KafkaPlayground.Listener.Consumers
{
    public abstract class BaseEventsConsumer
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly KafkaConfiguration _kafkaConfiguration;

        public BaseEventsConsumer(KafkaConfiguration configuration)
        {
            _kafkaConfiguration = configuration;

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

        public async void StartConsume()
        {
            _consumer.Subscribe(_kafkaConfiguration.Topic);
            Console.WriteLine($"Subscribed to {_kafkaConfiguration.Topic}");
            while (true)
            {
                try
                {
                    var consumeResult = _consumer.Consume();

                    Consume(consumeResult.Message.Value);

                    _consumer.Commit(consumeResult);
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error consuming message: {e.Error.Reason}");
                }
            }
        }

        public abstract void Consume(string receivedMessage);

        public void StopConsume()
        {
            _consumer.Close();
        }
    }
}