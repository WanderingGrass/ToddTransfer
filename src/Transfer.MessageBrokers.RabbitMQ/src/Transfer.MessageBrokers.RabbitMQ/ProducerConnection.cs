using RabbitMQ.Client;

namespace Transfer.MessageBrokers.RabbitMQ
{
    public sealed class ProducerConnection
    {
        public IConnection Connection { get; }

        public ProducerConnection(IConnection connection)
        {
            Connection = connection;
        }
    }
}