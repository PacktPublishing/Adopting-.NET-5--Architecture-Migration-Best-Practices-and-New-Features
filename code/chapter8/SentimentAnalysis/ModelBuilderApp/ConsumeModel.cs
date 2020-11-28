//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ModelBuilderApp
//{
//    public class ConsumeModel
//    {
//        private static Lazy<PredictionEngine<SentimentInput, SentimentOutput>> PredictionEngine = new Lazy<PredictionEngine<SentimentInput, SentimentOutput>>(CreatePredictionEngine);

//        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
//        // Method for consuming model in your app
//        public static SentimentOutput Predict(SentimentInput input)
//        {
//            SentimentOutput result = PredictionEngine.Value.Predict(input);
//            return result;
//        }

//        public static PredictionEngine<SentimentInput, SentimentOutput> CreatePredictionEngine()
//        {
//            // Create new MLContext
//            MLContext mlContext = new MLContext();

//            // Load model & create prediction engine
//            string modelPath = @"C:\Users\avail\AppData\Local\Temp\MLVSTools\ConsoleApp2ML\ConsoleApp2ML.Model\MLModel.zip";
//            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
//            var predEngine = mlContext.Model.CreatePredictionEngine<SentimentInput, SentimentOutput>(mlModel);

//            return predEngine;
//        }
//    }
//}
