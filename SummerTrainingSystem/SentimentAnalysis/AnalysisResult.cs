using Microsoft.ML.Data;
using System.Collections.Generic;

namespace SummerTrainingSystem.SentimentAnalysis
{
    public class AnalysisResult
    {
        public CalibratedBinaryClassificationMetrics Metrics { get; set; }
        public IEnumerable<SentimentPrediction> PredictedResults { get; set; }
    }
}
