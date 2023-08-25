using FindMusic.Models;
using FindMusic.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
	public class SeriesController : Controller
	{
		private readonly ISeriesRepository _seriesRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _hostEnvironment;

		public SeriesController(ISeriesRepository seriesRepository, UserManager<ApplicationUser> userManager,
			IWebHostEnvironment hostEnvironment)
		{
			_seriesRepository = seriesRepository;
			_userManager = userManager;
			_hostEnvironment = hostEnvironment;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
