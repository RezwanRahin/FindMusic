using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
	public class AdministrationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
