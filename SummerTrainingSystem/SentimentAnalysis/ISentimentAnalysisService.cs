using System.Collections.Generic;

namespace SummerTrainingSystem.SentimentAnalysis
{
    public interface ISentimentAnalysisService
    {
        AnalysisResult WatchAndLearn(IEnumerable<SentimentData> sentiments);
    }
}
