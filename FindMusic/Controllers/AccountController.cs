using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
