using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace SentimentAnalysis.Models
{
    public class SentimentInput
    {
        [ColumnName("text"), LoadColumn(0)]
        public string Text { get; set; }

        [ColumnName("sentiment"), LoadColumn(1)]
        public string Sentiment { get; set; }
    }
}
