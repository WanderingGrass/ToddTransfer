using System;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Transfer.MessageBrokers.RabbitMQ.Plugins
{
    internal interface IRabbitMqPluginsExecutor
    {
        Task ExecuteAsync(Func<object, object, BasicDeliverEventArgs, Task> successor,
            object message, object correlationContext, BasicDeliverEventArgs args);
    }
}