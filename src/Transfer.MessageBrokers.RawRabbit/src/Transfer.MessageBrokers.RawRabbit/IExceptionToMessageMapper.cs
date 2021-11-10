using System;

namespace Transfer.MessageBrokers.RawRabbit
{
    public interface IExceptionToMessageMapper
    {
        object Map(Exception exception, object message);
    }
}