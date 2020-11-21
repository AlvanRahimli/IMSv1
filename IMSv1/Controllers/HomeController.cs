using Microsoft.AspNetCore.Mvc;

namespace IMSv1.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}