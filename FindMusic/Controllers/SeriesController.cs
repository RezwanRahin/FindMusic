using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using FindMusic.ViewModels.SeriesViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FindMusic.Extensions;

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

		[HttpPost]
		public async Task<IActionResult> Create(CreateSeriesViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await _userManager.GetUserAsync(User);

			var series = new Series
			{
				Name = model.Name,
				Slug = model.Name.Slugify(),
				PhotoPath = model.Photo.ProcessUploadedFile(_hostEnvironment),
				User = user,
				ApplicationUserId = user.Id
			};

			await _seriesRepository.Add(series);

			return RedirectToAction("Details", new { slug = series.Slug });
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("[controller]/[action]/{slug}")]
		public async Task<IActionResult> Details(string slug)
		{
			var series = await _seriesRepository.GetSeriesBySlugWithRelatedData(slug);

			if (series == null)
			{
				Response.StatusCode = 404;
				ViewBag.ErrorMessage = $"Series like {slug} cannot be found";
				return View("NotFound");
			}

			var model = new SeriesDetailsViewModel
			{
				Id = series.Id,
				Name = series.Name,
				Slug = series.Slug,
				Poster = new PosterViewModel(series.PhotoPath),
				Contributor = new ContributorViewModel(series.User),

				Seasons = new List<int>()
			};

			foreach (var season in series.Seasons)
			{
				model.Seasons.Add(season.Number);
			}

			return View(model);
		}
	}
}
