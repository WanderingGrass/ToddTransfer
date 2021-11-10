using Transfer.MessageBrokers.RabbitMQ;
using Transfer.Tracing.Jaeger.RabbitMQ.Plugins;

namespace Transfer.Tracing.Jaeger.RabbitMQ
{
    public static class Extensions
    {
        public static IRabbitMqPluginsRegistry AddJaegerRabbitMqPlugin(this IRabbitMqPluginsRegistry registry)
        {
            registry.Add<JaegerPlugin>();
            return registry;
        }
    }
}