using System.Collections.Generic;

namespace Transfer.MessageBrokers.RabbitMQ.Plugins
{
    internal interface IRabbitMqPluginsRegistryAccessor
    {
        LinkedList<RabbitMqPluginChain> Get();
    }
}