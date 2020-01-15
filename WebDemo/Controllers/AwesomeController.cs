using Microsoft.AspNetCore.Mvc;

namespace WebDemo.Controllers
{
    public class AwesomeController : Controller
    {
        public IActionResult Error(string traceId)
        {
            return View("Error",traceId);
        }
    }
}