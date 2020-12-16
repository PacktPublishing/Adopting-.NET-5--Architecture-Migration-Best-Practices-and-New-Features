using RabbitMQ.Client;

namespace primecalculator.Messaging
{
    public interface IMQClient
    {
        IModel CreateChannel();
    }
}
