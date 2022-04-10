using Microsoft.AspNetCore.Hosting;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Generic;
using System.IO;
using static Microsoft.ML.DataOperationsCatalog;

namespace SummerTrainingSystem.SentimentAnalysis
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly IWebHostEnvironment _env;
        public SentimentAnalysisService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public AnalysisResult WatchAndLearn(IEnumerable<SentimentData> sentiments)
        {
            MLContext mlContext = new MLContext();
            TrainTestData splitDataView = LoadData(mlContext);
            ITransformer model = BuildAndTrainModel(mlContext, splitDataView.TrainSet);
            var evaluationResult = Evaluate(mlContext, model, splitDataView.TestSet);
            var predictedResults = UseModelWithBatchItems(mlContext, model, sentiments);

            return new AnalysisResult
            {
                Metrics = evaluationResult,
                PredictedResults = predictedResults
            };
        }
        private TrainTestData LoadData(MLContext mlContext)
        {
            string _dataPath = Path.Combine(_env.WebRootPath, "AnalysisData\\trainingset.txt");
            // The LoadFromTextFile() method defines the data schema and reads in the file.
            // It takes in the data path variables and returns an IDataView.
            IDataView dataView = mlContext.Data.LoadFromTextFile<SentimentData>(_dataPath, hasHeader: false);

            // The previous code uses the TrainTestSplit() method to split the loaded dataset into train and test datasets
            // and return them in the DataOperationsCatalog.TrainTestData class.
            // Specify the test set percentage of data with the testFractionparameter.
            // The default is 10%, in this case you use 20% to evaluate more data.
            TrainTestData splitDataView = mlContext.Data.TrainTestSplit(dataView, 0.2);

            return splitDataView;
        }

        private ITransformer BuildAndTrainModel(MLContext mlContext, IDataView splitTrainSet)
        {
            var estimator = mlContext.Transforms.
                Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(SentimentData.SentimentText))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));
            return estimator.Fit(splitTrainSet);
        }

        private CalibratedBinaryClassificationMetrics Evaluate(MLContext mlContext, ITransformer model, IDataView splitTestSet)
        {
            IDataView predictions = model.Transform(splitTestSet);
            CalibratedBinaryClassificationMetrics metrics = mlContext.BinaryClassification.Evaluate(predictions, "Label");
            return metrics;
        }

        private IEnumerable<SentimentPrediction> UseModelWithBatchItems(MLContext mlContext, ITransformer model, IEnumerable<SentimentData> sentiments)
        {
            IDataView batchComments = mlContext.Data.LoadFromEnumerable(sentiments);

            IDataView predictions = model.Transform(batchComments);

            // Use model to predict whether comment data is Positive (1) or Negative (0).
            IEnumerable<SentimentPrediction> predictedResults = mlContext.Data
                .CreateEnumerable<SentimentPrediction>(predictions, reuseRowObject: false);

            return predictedResults;
        }

        //private void UseModelWithSingleItem(MLContext mlContext, ITransformer model)
        //{
        //    PredictionEngine<SentimentData, SentimentPrediction> predictionFunction = mlContext.Model
        //        .CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
        //    SentimentData sampleStatement = new SentimentData
        //    {
        //        SentimentText = "This was a very bad steak"
        //    };
        //    var resultPrediction = predictionFunction.Predict(sampleStatement);
        //}
    }
}
