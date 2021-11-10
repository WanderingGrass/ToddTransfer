using System.Threading.Tasks;
using Transfer;
using Transfer.CQRS.Events;
using Transfer.Discovery.Consul;
using Transfer.LoadBalancing.Fabio;
using Transfer.Logging;
using Transfer.MessageBrokers.CQRS;
using Transfer.MessageBrokers.RabbitMQ;
using Transfer.Metrics.Prometheus;
using Transfer.Persistence.Redis;
using Transfer.Tracing.Jaeger;
using Transfer.Tracing.Jaeger.RabbitMQ;
using Transfer.WebApi;
using Transferor.Services.Deliveries.Events.External;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Transferor.Services.Deliveries
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
                        .AddConsul()
                        .AddFabio()
                        .AddJaeger()
                        .AddEventHandlers()
                        .AddRedis()
                        .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                        .AddPrometheus()
                        .AddWebApi()
                        .Build())
                    .Configure(app => app
                        .UseTransfer()
                        .UsePrometheus()
                        .UseErrorHandler()
                        .UseEndpoints(endpoints => endpoints
                            .Get("", ctx => ctx.Response.WriteAsync("Deliveries Service"))
                            .Get("ping", ctx => ctx.Response.WriteAsync("pong")))
                        .UseJaeger()
                        .UseRabbitMq()
                        .SubscribeEvent<OrderCreated>())
                    .UseLogging();
            });
    }
}
