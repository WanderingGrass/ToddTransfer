using System;

namespace Transfer.MessageBrokers.RabbitMQ
{
    public interface IExceptionToMessageMapper
    {
        object Map(Exception exception, object message);
    }
}