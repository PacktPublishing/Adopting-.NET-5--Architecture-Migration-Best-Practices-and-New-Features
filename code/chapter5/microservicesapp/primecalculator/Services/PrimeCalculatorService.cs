using Microsoft.Extensions.Caching.Distributed;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using microservicesapp;
using System;
using System.Threading.Tasks;
using primecalculator.Messaging;

namespace primecalculator
{
    public class PrimeCalculatorService : PrimeCalculator.PrimeCalculatorBase
    {
        private readonly ILogger<PrimeCalculatorService> _logger;
        private readonly IDistributedCache _cache;
        private readonly IMessageQueueSender _mqSender;

        private readonly ValueTuple<bool, bool> CACHE_HIT_SUCCESS = new ValueTuple<bool, bool>(true, true);
        private readonly PrimeReply TRUE_RESULT = new PrimeReply { IsPrime = true };
        private readonly PrimeReply FALSE_RESULT = new PrimeReply { IsPrime = false };

        private readonly string _queueName;

        public PrimeCalculatorService(ILogger<PrimeCalculatorService> logger, IDistributedCache cache, IMessageQueueSender mqSender)
        {
            _logger = logger;
            _cache = cache;
            _mqSender = mqSender;

            _queueName = Environment.GetEnvironmentVariable("RABBIT_QUEUE");

            //Debug time only - when localmachine debug without containers, without tye
            _queueName = string.IsNullOrWhiteSpace(_queueName) ? "primes" : _queueName;
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
        public override async Task<PrimeReply> IsItPrime(PrimeRequest request, ServerCallContext context)
        {
            if (request == null)
                return FALSE_RESULT;

            if (request.Number <= 0) return FALSE_RESULT;
            else if (request.Number == 1 || request.Number == 2) return TRUE_RESULT;
            else if (request.Number % 2 == 0) return FALSE_RESULT;
            else
            {
                var answerFromCache = await GetFromCache(request.Number);
                if (answerFromCache == CACHE_HIT_SUCCESS) return TRUE_RESULT;

                var boundary = (int)Math.Floor(Math.Sqrt(request.Number));

                for (int i = 3; i <= boundary; i += 2)
                    if (request.Number % i == 0)
                        return FALSE_RESULT;

                await SetThePrimeInCache(request);

                _mqSender.Send(_queueName, request.Number.ToString());
                return TRUE_RESULT;
            }
        }

        /// <summary>
        /// Get the answer from the cache if it exists.
        /// Consider valid answer if cache is a hit indicated by the return value (true,true).
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns>false,false if cache is a miss</returns>
        private async Task<ValueTuple<bool, bool>> GetFromCache(long input)
        {
            var answerHit = await _cache.GetStringAsync(input.ToString());
            if (answerHit == null) return new ValueTuple<bool, bool>(false, false);

            return new ValueTuple<bool, bool>(true, true); //if cache is a hit, then it is a prime number, no need to verify value of "answerHit"
        }

        private async Task SetThePrimeInCache(PrimeRequest request)
        {
            _logger.LogInformation("Setting the prime number in the cache: " + request.Number);

            await _cache.SetStringAsync(request.Number.ToString(), "true", new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
        }
    }
}
