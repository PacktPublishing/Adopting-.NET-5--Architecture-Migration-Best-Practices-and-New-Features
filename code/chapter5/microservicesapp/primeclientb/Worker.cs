using System;
using System.Threading;
using System.Threading.Tasks;
using microservicesapp;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace primeclientb
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly PrimeCalculator.PrimeCalculatorClient _primeClient;

        public Worker(ILogger<Worker> logger, PrimeCalculator.PrimeCalculatorClient primeClient)
        {
            _logger = logger;
            _primeClient = primeClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Giving time for all other services/dependencies to be warmed up (ex: RabbitMQ takes time to boot up)
            await Task.Delay(TimeSpan.FromSeconds(33), stoppingToken);
            _logger.LogInformation("Starting to send Prime number requests.......");

            long input = 100000; //Client B starts from 100K

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var response = await _primeClient.IsItPrimeAsync(new PrimeRequest { Number = input });
                    _logger.LogInformation($"Is {input} a Prime number? Service tells us: {response.IsPrime}\r");
                }
                catch (Exception ex)
                {
                    if (stoppingToken.IsCancellationRequested) return;
                    _logger.LogError(-1, ex, "Error occurred while calling IsItPrimeAsync() but will continue..");
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); //just adding some delay in case of error..
                }

                input++;

                if (input >= 1000000) break; //stops itself i.e. background execution after 1 million'th number/times

                if (stoppingToken.IsCancellationRequested) break;

                await Task.Delay(TimeSpan.FromMilliseconds(10), stoppingToken);
            }
        }
    }
}
