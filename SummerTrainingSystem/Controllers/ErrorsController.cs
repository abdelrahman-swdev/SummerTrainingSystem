using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SummerTrainingSystem.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("errors/{statuscode}")]
        public IActionResult HandleErrors(int statuscode)
        {
            //var result = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (statuscode == 404) {
                //_logger.LogWarning($"404 error occured on Path {result.OriginalPath} , queryString =  {result.OriginalQueryString}");
                return View("NotFound");
            }
            return BadRequest();
        }

        [Route("errors")]
        [AllowAnonymous]
        public IActionResult HandleExceptions()
        {
            //var exceptionData = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //_logger.LogError($"Path {exceptionData.Path} threw an exception {exceptionData.Error}");
            return View("Error");
        }
    }
}
