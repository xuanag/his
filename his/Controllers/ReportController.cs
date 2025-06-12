using Microsoft.AspNetCore.Mvc;

namespace his.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Master()
        {
            return View();
        }

        public IActionResult Quality()
        {
            return View();
        }
    }
}
