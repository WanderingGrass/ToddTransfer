using RabbitMQ.Client;

namespace Transfer.MessageBrokers.RabbitMQ
{
    public sealed class ConsumerConnection
    {
        public IConnection Connection { get; }

        public ConsumerConnection(IConnection connection)
        {
            Connection = connection;
        }
    }
}