using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
