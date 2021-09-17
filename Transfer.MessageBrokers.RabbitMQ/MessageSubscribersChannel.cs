using System.Threading.Channels;
using Transfer.MessageBrokers.RabbitMQ.Subscribers;

namespace Transfer.MessageBrokers.RabbitMQ
{
    internal class MessageSubscribersChannel
    {
        private readonly Channel<IMessageSubscriber> _channel = Channel.CreateUnbounded<IMessageSubscriber>();

        public ChannelReader<IMessageSubscriber> Reader => _channel.Reader;
        public ChannelWriter<IMessageSubscriber> Writer => _channel.Writer;
    }
}