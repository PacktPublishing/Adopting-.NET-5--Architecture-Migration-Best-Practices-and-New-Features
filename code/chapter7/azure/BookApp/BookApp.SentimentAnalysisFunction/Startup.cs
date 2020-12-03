using BookApp.SentimentAnalysisFunction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace BookApp.SentimentAnalysisFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ISentimentAnaysisService, SentimentAnaysisService>();
        }

    }
}
