using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels.SeasonViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeasonController(ISeriesRepository seriesRepository, ISeasonRepository seasonRepository,
            UserManager<ApplicationUser> userManager)
        {
            _seriesRepository = seriesRepository;
            _seasonRepository = seasonRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> DoesNumberExist(string seriesSlug, int number)
        {
            var season = await _seasonRepository.GetSeason(number, seriesSlug);
            return season == null ? Json(true) : Json($"Season with Number = {number} already exists!");
        }

        [HttpGet]
        public async Task<IActionResult> Create(string seriesSlug)
        {
            var series = await _seriesRepository.GetSeriesBySlug(seriesSlug);

            if (series == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Series like {seriesSlug} cannot be found";
                return View("NotFound");
            }

            var model = new CreateSeasonViewModel
            {
                SeriesName = series.Name,
                SeriesSlug = series.Slug,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSeasonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var series = await _seriesRepository.GetSeriesBySlug(model.SeriesSlug);
            if (series == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Series like {model.SeriesSlug} cannot be found";
                return View("NotFound");
            }

            var user = await _userManager.GetUserAsync(User);

            var season = new Season
            {
                Number = model.Number,
                Year = model.Year,
                Series = series,
                User = user
            };

            await _seasonRepository.Add(season);

            return RedirectToAction("Details", "Series", new { slug = series.Slug });
        }
    }
}
