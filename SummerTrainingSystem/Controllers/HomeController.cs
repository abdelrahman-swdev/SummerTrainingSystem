using Microsoft.AspNetCore.Mvc;

namespace SummerTrainingSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
