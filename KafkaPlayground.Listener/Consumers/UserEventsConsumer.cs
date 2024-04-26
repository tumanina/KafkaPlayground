using KafkaPlayground.Configuration;

namespace KafkaPlayground.Listener.Consumers
{
    public class UserEventsConsumer : BaseEventsConsumer, IConsumer
    {
        public UserEventsConsumer(UserEventsConfiguration configuration)
            : base(configuration)
        { }

        public override void Consume(string receivedMessage)
        {
            Console.WriteLine($"Received user event: {receivedMessage}");
        }
    }
}