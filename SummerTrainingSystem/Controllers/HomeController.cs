using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SummerTrainingSystem.Models;
using System.Diagnostics;

namespace SummerTrainingSystem.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
