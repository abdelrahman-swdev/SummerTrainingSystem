using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerTrainingSystem.Models;
using SummerTrainingSystem.SentimentAnalysis;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Controllers
{
    [Route("analysis")]
    [Authorize(Roles ="Admin, Supervisor")]
    public class AnalysisController : Controller
    {
        private readonly ISentimentAnalysisService _analysisService;
        private readonly IUnitOfWork _unitOfWork;

        public AnalysisController(ISentimentAnalysisService analysisService, IUnitOfWork unitOfWork)
        {
            _analysisService = analysisService;
            _unitOfWork = unitOfWork;
        }

        [Route("sentiment-analysis")]
        public async Task<IActionResult> Index()
        {
            var companies = await _unitOfWork.GenericRepository<HrCompany>().ListAsync(c => true, source => source.Include(c => c.Comments));
            var model = new List<SentimentAnalysisVM>();
            foreach (var company in companies)
            {
                if(company.Comments.Count > 0)
                {
                    var sentiments = new List<SentimentData>();
                    foreach (var comment in company.Comments)
                    {
                        sentiments.Add(new SentimentData { SentimentText = comment.Message });
                    }
                    var result = _analysisService.WatchAndLearn(sentiments);

                    var positive = Convert.ToDecimal(result.PredictedResults.Where(p => Convert.ToBoolean(p.Prediction)).Count());
                    var all = Convert.ToDecimal(result.PredictedResults.Count());
                    var calculatedPercentage = Convert.ToDecimal(positive / all) * 100;

                    model.Add(new SentimentAnalysisVM { 
                        CompanyId = company.Id,
                        Name = company.Name, 
                        ProfilePictureUrl = company.ProfilePictureUrl, 
                        AnalysisResult = result,
                        CalculatedPercentage = (int)calculatedPercentage
                    });

                }
                else
                {
                    model.Add(new SentimentAnalysisVM
                    {
                        CompanyId = company.Id,
                        Name = company.Name,
                        ProfilePictureUrl = company.ProfilePictureUrl,
                        AnalysisResult = null,
                        CalculatedPercentage = -1
                    });
                }
            }
            return View(model);
        }
    }
}
