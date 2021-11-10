using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Transfer.CQRS.Commands;
using Transfer.CQRS.Events;
using Transfer.Logging.CQRS.Decorators;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Transfer.Logging.CQRS
{
    public static class Extensions
    {
        public static ITransferBuilder AddCommandHandlersLogging(this ITransferBuilder builder, Assembly assembly = null)
            => builder.AddHandlerLogging(typeof(ICommandHandler<>), typeof(CommandHandlerLoggingDecorator<>), assembly);

        public static ITransferBuilder AddEventHandlersLogging(this ITransferBuilder builder, Assembly assembly = null)
            => builder.AddHandlerLogging(typeof(IEventHandler<>), typeof(EventHandlerLoggingDecorator<>), assembly);

        private static ITransferBuilder AddHandlerLogging(this ITransferBuilder builder, Type handlerType,
            Type decoratorType, Assembly assembly = null)
        {
            assembly ??= Assembly.GetCallingAssembly();

            var handlers = assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType))
                .ToList();
            
            handlers.ForEach(ch => GetExtensionMethods()
                .FirstOrDefault(mi => !mi.IsGenericMethod && mi.Name == "TryDecorate")?
                .Invoke(builder.Services, new object[]
                {
                    builder.Services,
                    ch.GetInterfaces().FirstOrDefault(),
                    decoratorType.MakeGenericType(ch.GetInterfaces().FirstOrDefault()?.GenericTypeArguments.First())
                }));

            return builder;
        }

        private static IEnumerable<MethodInfo> GetExtensionMethods()
        {
            var types = typeof(ReplacementBehavior).Assembly.GetTypes();

            var query = from type in types
                where type.IsSealed && !type.IsGenericType && !type.IsNested
                from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                where method.IsDefined(typeof(ExtensionAttribute), false)
                where method.GetParameters()[0].ParameterType == typeof(IServiceCollection)
                select method;
            return query;
        }
    }
}