namespace KafkaPlayground.Producer
{
    public interface IProducer
    {
        public Task SendMessage(string message);
    }
}
