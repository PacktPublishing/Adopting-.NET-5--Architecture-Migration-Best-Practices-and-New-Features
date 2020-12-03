using Grpc.Core;
using microservicesapp;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace primecalculator
{
    public class PrimeCalculatorService : PrimeCalculator.PrimeCalculatorBase
    {
        private readonly ILogger<PrimeCalculatorService> _logger;
        public PrimeCalculatorService(ILogger<PrimeCalculatorService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// A straight forward implementation to calculate a prime number.
        /// Restrict yourself to the max calc for the square-root of the given input number
        /// 
        /// Input: A number
        /// Response: true if it is a prime number, false otherwise
        /// </summary>
        /// <param name="request">a number</param>
        /// <param name="context"></param>
        /// <returns>true if it is a prime number, false otherwise</returns>
        public override Task<PrimeReply> IsItPrime(PrimeRequest request, ServerCallContext context)
        {
            var isPrime = false;

            if (request == null)
                return Task.FromResult(new PrimeReply { IsPrime = isPrime });

            if (request.Number <= 1) isPrime = false;
            else if (request.Number == 2) isPrime = true;
            else if (request.Number % 2 == 0) isPrime = false;
            else
            {
                var boundary = (int)Math.Floor(Math.Sqrt(request.Number));

                for (int i = 3; i <= boundary; i += 2)
                    if (request.Number % i == 0)
                        return Task.FromResult(new PrimeReply { IsPrime = false }); ;

                isPrime = true;
            }

            return Task.FromResult(new PrimeReply { IsPrime = isPrime });
        }
    }
}
