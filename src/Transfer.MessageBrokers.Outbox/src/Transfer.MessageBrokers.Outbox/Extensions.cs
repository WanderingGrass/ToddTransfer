using System;
using Transfer.MessageBrokers.Outbox.Configurators;
using Transfer.MessageBrokers.Outbox.Outbox;
using Transfer.MessageBrokers.Outbox.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace Transfer.MessageBrokers.Outbox
{
    public static class Extensions
    {
        private const string SectionName = "outbox";
        private const string RegistryName = "messageBrokers.outbox";

        public static ITransferBuilder AddMessageOutbox(this ITransferBuilder builder,
            Action<IMessageOutboxConfigurator> configure = null, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

            var options = builder.GetOptions<OutboxOptions>(sectionName);
            builder.Services.AddSingleton(options);
            var configurator = new MessageOutboxConfigurator(builder, options);

            if (configure is null)
            {
                configurator.AddInMemory();
            }
            else
            {
                configure(configurator);
            }

            if (!options.Enabled)
            {
                return builder;
            }

            builder.Services.AddHostedService<OutboxProcessor>();

            return builder;
        }

        public static IMessageOutboxConfigurator AddInMemory(this IMessageOutboxConfigurator configurator,
            string mongoSectionName = null)
        {
            configurator.Builder.Services.AddTransient<IMessageOutbox, InMemoryMessageOutbox>();

            return configurator;
        }
    }
}