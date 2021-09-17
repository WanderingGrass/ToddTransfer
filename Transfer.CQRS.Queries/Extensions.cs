using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.CQRS.Queries.Dispatchers;
using Transfer.Types;

namespace Transfer.CQRS.Queries
{
    public static class Extensions
    {
        public static ITransferBuilder AddQueryHandlers(this ITransferBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
                        .WithoutAttribute(typeof(DecoratorAttribute)))//匹配未定Attribute属性的所有类型
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
