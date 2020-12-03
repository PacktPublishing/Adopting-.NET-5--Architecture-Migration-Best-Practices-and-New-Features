using BookApp.SentimentAnalysisFunction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using SentimentAnalysis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace BookApp.SentimentAnalysisFunction
{
    public class Startup : FunctionsStartup
    {
        private readonly string environment;
        private readonly string modelPath;

        public Startup()
        {
            environment = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");

            if (environment == "Development")
            {
                modelPath = Path.Combine("MLModels", "sentiment_analysis_model.zip");
            }
            else
            {
                string deploymentPath = @"D:\home\site\wwwroot\";
                modelPath = Path.Combine(deploymentPath, "MLModels", "sentiment_analysis_model.zip");
            }
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ISentimentAnaysisService, SentimentAnaysisService>();
            builder.Services.AddPredictionEnginePool<SentimentInput, SentimentOutput>()
                .FromFile(modelName: "SentimentAnalysisModel", filePath: modelPath, watchForChanges: true);
        }

    }
}
