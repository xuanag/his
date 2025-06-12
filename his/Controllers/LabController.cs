using Microsoft.AspNetCore.Mvc;

namespace his.Controllers
{
    public class LabController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
