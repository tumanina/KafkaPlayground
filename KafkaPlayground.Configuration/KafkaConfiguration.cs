namespace KafkaPlayground.Configuration
{
    public class KafkaConfiguration
    {
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Topic { get; set; }
        public string ConsumerGroupId { get; set; }
    }
}