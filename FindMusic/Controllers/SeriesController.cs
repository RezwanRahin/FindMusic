using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var allSeries = await _seriesRepository.GetAllSeries();

			var model = allSeries.Select(series => new CardViewModel
			{
				Name = series.Name,
				Slug = series.Slug,
				PhotoPath = PosterViewModel.GetPhoto(series.PhotoPath)
			});

			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
	}
}
