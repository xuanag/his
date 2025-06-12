using Microsoft.AspNetCore.Mvc;

namespace his.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("danh-muc/may-xet-nghiem")]
        public IActionResult Machine()
        {
            return View();
        }
    }
}
