using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookApp.SentimentAnalysisFunction
{
    public class SentimentFunction
    {
        private readonly ISentimentAnaysisService sentimentAnaysisService;
        public SentimentFunction(ISentimentAnaysisService sentimentAnaysisService)
        {
            this.sentimentAnaysisService = sentimentAnaysisService;
        }

        [FunctionName("GetSentiment")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string text = req.Query["text"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            text = text ?? data?.text;


            var result = sentimentAnaysisService.GetSentiment(text);

            return new OkObjectResult(result);
        }
    }
}
