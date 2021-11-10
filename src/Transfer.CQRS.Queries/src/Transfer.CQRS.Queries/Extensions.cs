using System;
using Transfer.CQRS.Queries.Dispatchers;
using Transfer.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Transfer.CQRS.Queries
{
    public static class Extensions
    {
        public static ITransferBuilder AddQueryHandlers(this ITransferBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
                        .WithoutAttribute(typeof(DecoratorAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }

        public static ITransferBuilder AddInMemoryQueryDispatcher(this ITransferBuilder builder)
        {
            builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            return builder;
        }
    }
}