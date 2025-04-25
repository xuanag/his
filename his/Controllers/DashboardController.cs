using Microsoft.AspNetCore.Mvc;

namespace his.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
