using System;
using Microsoft.ML.Data;

namespace SentimentAnalysis.Models
{
    public class SentimentOutput
    {
        [ColumnName("PredictedLabel")]
        public String Prediction { get; set; }
        public float[] Score { get; set; }
    }
}
