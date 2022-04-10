using SummerTrainingSystem.SentimentAnalysis;

namespace SummerTrainingSystem.Models
{
    public class SentimentAnalysisVM
    {
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string ProfilePictureUrl { get; set; }
        public int CalculatedPercentage { get; set; }
        public AnalysisResult AnalysisResult { get; set; }
    }
}
