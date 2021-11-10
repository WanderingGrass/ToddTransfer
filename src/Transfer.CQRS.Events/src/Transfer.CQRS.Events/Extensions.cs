using System;
using Transfer.CQRS.Events.Dispatchers;
using Transfer.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Transfer.CQRS.Events
{
    public static class Extensions
    {
        public static ITransferBuilder AddEventHandlers(this ITransferBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))
                        .WithoutAttribute(typeof(DecoratorAttribute))) 
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }

        public static ITransferBuilder AddInMemoryEventDispatcher(this ITransferBuilder builder)
        {
            builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>();
            return builder;
        }
    }
}