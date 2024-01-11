using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels;
using FindMusic.ViewModels.SeasonViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/{seriesSlug}/[action]/season-{number:int}")]
        public async Task<IActionResult> Details(string seriesSlug, int number)
        {
            var season = await _seasonRepository.GetSeasonWithRelatedData(number, seriesSlug);

            if (season == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Season with Number = {number} & Series like {seriesSlug} cannot be found";
                return View("NotFound");
            }

            var model = new SeasonDetailsViewModel
            {
                Id = season.Id,
                Number = season.Number,
                Year = season.Year.Year,
                Contributor = new ContributorViewModel(season.User),

                Series = new RelatedSeriesViewModel
                {
                    Name = season.Series.Name,
                    Slug = season.Series.Slug,
                    Poster = new PosterViewModel(season.Series.PhotoPath)
                },

                Episodes = new List<int>()
            };

            foreach (var episode in season.Episodes)
            {
                model.Episodes.Add(episode.Number);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string seriesSlug, int number)
        {
            var season = await _seasonRepository.GetSeason(number, seriesSlug);

            if (season == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Season with Number = {number} & Series like {seriesSlug} cannot be found";
                return View("NotFound");
            }

            var model = new UpdateSeasonViewModel
            {
                Id = season.Id,
                Number = season.Number,
                Year = season.Year,
                SeriesName = season.Series.Name,
                SeriesSlug = season.Series.Slug
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSeasonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var season = await _seasonRepository.GetSeasonWithRelatedData(model.Id);

            if (season == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Season with Id = {model.Id} cannot be found";
                return View("NotFound");
            }

            season.Number = model.Number;
            season.Year = model.Year;

            await _seasonRepository.Update(season);

            return RedirectToAction("Details", new { seriesSlug = season.Series.Slug, number = season.Number });
        }
    }
}
