using System;
using System.Threading.Tasks;
using Transfer;
using Transfer.CQRS.Commands;
using Transfer.CQRS.Events;
using Transfer.CQRS.Queries;
using Transfer.Discovery.Consul;
using Transfer.Docs.Swagger;
using Transfer.HTTP;
using Transfer.LoadBalancing.Fabio;
using Transfer.Logging;
using Transfer.MessageBrokers.CQRS;
using Transfer.MessageBrokers.Outbox;
using Transfer.MessageBrokers.Outbox.Mongo;
using Transfer.MessageBrokers.RabbitMQ;
using Transfer.Metrics.Prometheus;
using Transfer.Persistence.MongoDB;
using Transfer.Persistence.Redis;
using Transfer.Secrets.Vault;
using Transfer.Tracing.Jaeger;
using Transfer.Tracing.Jaeger.RabbitMQ;
using Transfer.WebApi;
using Transfer.WebApi.CQRS;
using Transfer.WebApi.Security;
using Transfer.WebApi.Swagger;
using Transferor.Services.Orders.Commands;
using Transferor.Services.Orders.Domain;
using Transferor.Services.Orders.DTO;
using Transferor.Services.Orders.Events.External;
using Transferor.Services.Orders.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Transferor.Services.Orders
{
    public class Program
    {
        public static Task Main(string[] args)
            => CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services => services
                        .AddTransfer()
                        .AddErrorHandler<ExceptionToResponseMapper>()
                        .AddServices()
                        .AddHttpClient()
                        .AddCorrelationContextLogging()
                        .AddConsul()
                        .AddFabio()
                        .AddJaeger()
                        .AddMongo()
                        .AddMongoRepository<Order, Guid>("orders")
                        .AddCommandHandlers()
                        .AddEventHandlers()
                        .AddQueryHandlers()
                        .AddInMemoryCommandDispatcher()
                        .AddInMemoryEventDispatcher()
                        .AddInMemoryQueryDispatcher()
                        .AddPrometheus()
                        .AddRedis()
                        .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                        .AddMessageOutbox(o => o.AddMongo())
                        .AddWebApi()
                        .AddSwaggerDocs()
                        .AddWebApiSwaggerDocs()
                        .Build())
                    .Configure(app => app
                        .UseTransfer()
                        .UserCorrelationContextLogging()
                        .UseErrorHandler()
                        .UsePrometheus()
                        .UseRouting()
                        .UseCertificateAuthentication()
                        .UseEndpoints(r => r.MapControllers())
                        .UseDispatcherEndpoints(endpoints => endpoints
                                .Get("", ctx => ctx.Response.WriteAsync("Orders Service"))
                                .Get("ping", ctx => ctx.Response.WriteAsync("pong"))
                                .Get<GetOrder, OrderDto>("orders/{orderId}")
                                .Post<CreateOrder>("orders",
                                    afterDispatch: (cmd, ctx) => ctx.Response.Created($"orders/{cmd.OrderId}")))
                        .UseJaeger()
                        .UseSwaggerDocs()
                        .UseRabbitMq()
                        .SubscribeEvent<DeliveryStarted>())
                    .UseLogging()
                    .UseVault();
            });
    }
}
