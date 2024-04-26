using KafkaPlayground.Configuration;

namespace KafkaPlayground.Listener.Consumers
{
    public class VehicleEventsConsumer : BaseEventsConsumer, IConsumer
    {
        public VehicleEventsConsumer(VehicleEventsConfiguration configuration)
            : base(configuration)
        { }

        public override void Consume(string receivedMessage)
        {
            Console.WriteLine($"Received vehicle event: {receivedMessage}");
        }
    }
}