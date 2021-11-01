using System.Collections.Generic;

namespace Transfer.MessageBrokers.RabbitMQ
{
    public interface IContextProvider
    {
        string HeaderName { get; }
        object Get(IDictionary<string, object> headers);
    }
}