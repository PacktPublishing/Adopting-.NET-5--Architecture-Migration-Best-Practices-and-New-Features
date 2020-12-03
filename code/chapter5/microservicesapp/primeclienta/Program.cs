using microservicesapp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace primeclienta
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configurationFromHostBuilderContext = hostContext.Configuration;

                    services.AddHostedService<Worker>();

                    services.AddGrpcClient<PrimeCalculator.PrimeCalculatorClient>(o =>
                    {
                        //primecalculator will be inject to configuration via Tye
                        o.Address = configurationFromHostBuilderContext.GetServiceUri("primecalculator");
                    });
                });
    }
}
