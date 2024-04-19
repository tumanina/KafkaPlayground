using Confluent.Kafka;
using KafkaPlayground.Configuration;
using Microsoft.Extensions.Options;

namespace KafkaPlayground.Producer
{
    public class UserEventsProducer: IProducer
    {
        private readonly IProducer<Null, string> _producer; 
        private readonly UserEventsConfiguration _kafkaConfiguration;

        public UserEventsProducer(IOptions<UserEventsConfiguration> configuration) 
        {
            _kafkaConfiguration = configuration.Value;
            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaConfiguration.Server,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = _kafkaConfiguration.Username,
                SaslPassword = _kafkaConfiguration.Password,
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendMessage(string message)
        {
            await _producer.ProduceAsync(_kafkaConfiguration.Topic, new Message<Null, string> { Value = message });
        }
    }
}
