using System;
using Transfer.CQRS.Commands.Dispatchers;
using Transfer.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Transfer.CQRS.Commands
{
    public static class Extensions
    {
        public static ITransferBuilder AddCommandHandlers(this ITransferBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>))
                        .WithoutAttribute(typeof(DecoratorAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }

        public static ITransferBuilder AddInMemoryCommandDispatcher(this ITransferBuilder builder)
        {
            builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            return builder;
        }
    }
}