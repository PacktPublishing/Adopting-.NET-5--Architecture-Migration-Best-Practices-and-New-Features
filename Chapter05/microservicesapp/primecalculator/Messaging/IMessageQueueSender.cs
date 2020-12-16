namespace primecalculator.Messaging
{
    public interface IMessageQueueSender
    {
        public void Send(string queueName, string message);
    }
}
