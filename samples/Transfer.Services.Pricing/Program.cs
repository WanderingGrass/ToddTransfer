using System.Threading.Tasks;
using Transfer;
using Transfer.Discovery.Consul;
using Transfer.LoadBalancing.Fabio;
using Transfer.Logging;
using Transfer.Metrics.Prometheus;
using Transfer.Tracing.Jaeger;
using Transfer.WebApi;
using Transfer.WebApi.Security;
using Transferor.Services.Pricing.DTO;
using Transferor.Services.Pricing.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Open.Serialization.Json;

namespace Transferor.Services.Pricing
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
                        .AddConsul()
                        .AddFabio()
                        .AddJaeger()
                        .AddPrometheus()
                        .AddWebApi()
                        .Build())
                    .Configure(app => app
                        .UseTransfer()
                        .UsePrometheus()
                        .UseErrorHandler()
                        .UseJaeger()
                        .UseCertificateAuthentication()
                        .UseAuthentication()
                        .UseAuthorization()
                        .UseEndpoints(endpoints => endpoints
                                .Get("", ctx => ctx.Response.WriteAsync("Pricing Service"))
                                .Get("ping", ctx => ctx.Response.WriteAsync("pong"))
                                .Get<GetOrderPricing>("orders/{orderId}/pricing", async (query, ctx) =>
                                    await ctx.RequestServices.GetRequiredService<IJsonSerializer>()
                                        .SerializeAsync(ctx.Response.Body, new PricingDto
                                        {
                                            OrderId = query.OrderId, TotalAmount = 20.50m
                                        }))))
                    .UseLogging();
            });
    }
}
