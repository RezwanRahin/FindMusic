using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    [Route("Account/[action]/{username?}")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
