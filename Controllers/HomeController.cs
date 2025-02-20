using Microsoft.AspNetCore.Mvc;

namespace IPLManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Home/Privacy
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
