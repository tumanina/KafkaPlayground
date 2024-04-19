namespace KafkaPlayground.Listener.Consumers
{
    public interface IConsumer
    {
        public void StartConsume();
        public void StopConsume();
    }
}
