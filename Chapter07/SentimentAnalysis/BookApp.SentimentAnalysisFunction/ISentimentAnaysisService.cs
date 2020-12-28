namespace BookApp.SentimentAnalysisFunction
{
    public interface ISentimentAnaysisService
    {
        Sentiment GetSentiment(string text);
    }
}