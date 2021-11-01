using System;

namespace Transfer.MessageBrokers.RabbitMQ
{
    public interface IConventions
    {
        Type Type { get; }
        string RoutingKey { get; }
        string Exchange { get; }
        string Queue { get; }
    }
}