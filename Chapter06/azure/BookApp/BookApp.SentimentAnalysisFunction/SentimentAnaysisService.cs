using Microsoft.AspNetCore.Builder;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BookApp.SentimentAnalysisFunction
{
    public class SentimentAnaysisService : ISentimentAnaysisService
    {

        private static string[] PositiveWords = {
            "accessible","accomplishment","accurate","accurately","achievable","achievement","acumen","admirable","admiration","admire","adorable","advantage","agreeable","amazing","amazingly","ambitious","ample","assure","authentic","avid","awesome","awesomeness","beautiful","best","blockbuster","brilliant","captivating","cheerful","clean","comfortable","complementary","complemented","complements","cool","correct","correctly","dependably","eagerness","easy","easy-to-use","effective","exceed","exceeds","excellent","fair","faultless","fine","friendly","great","genius","happiness","happy","helpful","hopeful","ideal","joyful","keen","love","magnificent"," nice","perfect","profound","promising","promoter","recommend","recommended","satisfied","wonderful","worthwhile","wow","wowed","yay"
        };
        private static string[] NegativeWords = {
            "abnormal","agony","alienated","ambiguity","angry","annoyed","bad","cheap","confusing","disagree","disappointing","disappointment","disappointments","expensive","fails","failure","fake","hassle","illogic","needless","negative","painful","unfortunately","unhappy","unhealthy","unhelpful","wrong"
        };
        public Sentiment GetSentiment(string text)
        {
            if (string.IsNullOrEmpty(text)) return Sentiment.Neutral;
            var words = text.Split(new char[] { ' ', ';', '-', ':', '.' });

            int negativeCount = 0;
            int positiveCount = 0;

            foreach (var word in words)
            {
                if (PositiveWords.Contains(word))
                    positiveCount++;
                if (NegativeWords.Contains(word))
                    negativeCount++;
            }

            var netPositiveWords = positiveCount - negativeCount;
            switch (netPositiveWords)
            {
                case int n when n > 0:
                    return Sentiment.Positive;
                case int n when n < 0:
                    return Sentiment.Negative;
                default:
                    return Sentiment.Neutral;

            }




        }
    }
}