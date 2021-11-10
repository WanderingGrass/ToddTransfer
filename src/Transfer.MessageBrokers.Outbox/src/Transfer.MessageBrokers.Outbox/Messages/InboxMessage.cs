using System;
using Transfer.Types;

namespace Transfer.MessageBrokers.Outbox.Messages
{
    public sealed class InboxMessage : IIdentifiable<string>
    {
        public string Id { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}