using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
	public class SeriesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
