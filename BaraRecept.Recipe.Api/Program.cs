using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Bergendahls.Template.Api
{
    public class Program
    {
        public static IMetricsRoot Metrics { get; set; }

        static void Main(string[] args)
        {
            Metrics = AppMetrics.CreateDefaultBuilder()
                .OutputMetrics.AsPrometheusPlainText()
                .Build();

            // Kestrel
            var builder = new WebHostBuilder()
                .SuppressStatusMessages(true)
                .UseKestrel(c => c.AddServerHeader = false)
                //.UseEnvironment("Staging")
                .ConfigureMetrics(Metrics)
                .UseMetrics(
                    options =>
                    {
                        options.EndpointOptions = endpointsOptions =>
                        {
                            endpointsOptions.MetricsTextEndpointOutputFormatter = 
                                Metrics.OutputMetricsFormatters.GetType<MetricsPrometheusTextOutputFormatter>();
                        };
                    })
                .UseMetricsWebTracking()
                .UseUrls("http://0.0.0.0:5000")
                .ConfigureLogging((context, logging) => {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .UseStartup<StartUp>();
            var host = builder.Build();
            host.Run();
        }
    }
}
