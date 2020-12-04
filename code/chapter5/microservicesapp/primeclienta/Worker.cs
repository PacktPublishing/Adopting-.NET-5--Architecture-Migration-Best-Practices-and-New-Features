using System;
using System.Threading;
using System.Threading.Tasks;
using microservicesapp;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace primeclienta
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
            long input = 3;

            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await _primeClient.IsItPrimeAsync(new PrimeRequest { Number = input });

                _logger.LogInformation($"Is {input} a Prime number? Service tells us: {response.IsPrime}\r");

                input++;

                if (stoppingToken.IsCancellationRequested) break;

                await Task.Delay(TimeSpan.FromMilliseconds(50), stoppingToken);
            }
        }
    }
}
