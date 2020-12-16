using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.ML;
using SentimentAnalysis.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BookApp.SentimentAnalysisFunction
{
    public class SentimentAnaysisService : ISentimentAnaysisService
    {
        private readonly PredictionEnginePool<SentimentInput, SentimentOutput> predictionEnginePool;

        public SentimentAnaysisService(PredictionEnginePool<SentimentInput, SentimentOutput> predictionEnginePool)
        {
            this.predictionEnginePool = predictionEnginePool;
        }

        public Sentiment GetSentiment(string text)
        {
            var input = new SentimentInput
            {
                Text = text
            };

            var prediction = predictionEnginePool.Predict(modelName: "SentimentAnalysisModel", example: input);

            var confidence = prediction.Prediction == "0" ? prediction.Score[0] : prediction.Score[1];
            if (confidence < 0.7)
                return Sentiment.Neutral;

            return (prediction.Prediction=="1") ? Sentiment.Positive : Sentiment.Negative;

        }
    }
}