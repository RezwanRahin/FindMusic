using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels;
using FindMusic.ViewModels.EpisodeViewModels;
using FindMusic.ViewModels.TimestampViewModels;
using FindMusic.ViewModels.TrackViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    public class EpisodeController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public EpisodeController(ISeasonRepository seasonRepository, IEpisodeRepository episodeRepository,
            UserManager<ApplicationUser> userManager)
        {
            _seasonRepository = seasonRepository;
            _episodeRepository = episodeRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> DoesNumberExist(string seriesSlug, int seasonNumber, int episodeNumber)
        {
            var episode = await _episodeRepository.GetEpisode(episodeNumber, seasonNumber, seriesSlug);
            return episode == null ? Json(true) : Json($"Episode with Number = {episodeNumber} already exists!");
        }

        [HttpGet]
        public async Task<IActionResult> Create(string seriesSlug, int seasonNumber)
        {
            var season = await _seasonRepository.GetSeason(seasonNumber, seriesSlug);

            if (season == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Season with Number = {seasonNumber} & Series like {seriesSlug} cannot be found";
                return View("NotFound");
            }

            var model = new CreateEpisodeViewModel
            {
                SeriesName = season.Series.Name,
                SeriesSlug = season.Series.Slug,
                SeasonNumber = season.Number
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEpisodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var season = await _seasonRepository.GetSeason(model.SeasonNumber, model.SeriesSlug);

            if (season == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Season with Number = {model.SeasonNumber} & Series like {model.SeriesSlug} cannot be found";
                return View("NotFound");
            }

            var user = await _userManager.GetUserAsync(User);


            var episode = new Episode
            {
                Number = model.Number,
                Name = model.Name,
                Season = season,
                User = user
            };

            await _episodeRepository.Add(episode);

            return RedirectToAction("Details", "Season", new { seriesSlug = season.Series.Slug, number = season.Number });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/{seriesSlug}/season-{seasonNumber:int}/[action]/episode-{episodeNumber:int}")]
        public async Task<IActionResult> Details(string seriesSlug, int seasonNumber, int episodeNumber)
        {
            var episode = await _episodeRepository.GetEpisodeWithRelatedData(episodeNumber, seasonNumber, seriesSlug);

            if (episode == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Episode of Number = {episodeNumber}, with Season = {seasonNumber}, and Series like {seriesSlug} cannot be found";
                return View("NotFound");
            }

            var model = new EpisodeDetailsViewModel
            {
                Id = episode.Id,
                Number = episode.Number,
                Name = episode.Name ?? "None",
                SeasonNumber = episode.Season.Number,
                Contributor = new ContributorViewModel(episode.User),

                Series = new RelatedSeriesViewModel
                {
                    Name = episode.Season.Series.Name,
                    Slug = episode.Season.Series.Slug,
                    Poster = new PosterViewModel(episode.Season.Series.PhotoPath),
                },

                Timestamps = new List<TimestampDetailsViewModel>()
            };

            foreach (var timestamp in episode.Timestamps)
            {
                var timestampDetailsViewModel = new TimestampDetailsViewModel
                {
                    Id = timestamp.Id,
                    Hour = timestamp.Hour,
                    Minute = timestamp.Minute,
                    Second = timestamp.Second,
                    Contributor = new ContributorViewModel(timestamp.User),

                    Tracks = new List<TrackDetailsViewModel>()
                };

                foreach (var track in timestamp.Tracks)
                {
                    var trackDetailsViewModel = new TrackDetailsViewModel
                    {
                        Id = track.Id,
                        Title = track.Title,
                        Url = track.Url,
                        Contributor = new ContributorViewModel(track.User),
                    };

                    timestampDetailsViewModel.Tracks.Add(trackDetailsViewModel);
                }

                model.Timestamps.Add(timestampDetailsViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string seriesSlug, int seasonNumber, int episodeNumber)
        {
            var episode = await _episodeRepository.GetEpisode(episodeNumber, seasonNumber, seriesSlug);

            if (episode == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Episode of Number = {episodeNumber}, with Season = {seasonNumber}, and Series like {seriesSlug} cannot be found";
                return View("NotFound");
            }

            var model = new UpdateEpisodeViewModel
            {
                Id = episode.Id,
                Number = episode.Number,
                Name = episode.Name,
                SeriesName = episode.Season.Series.Name,
                SeriesSlug = episode.Season.Series.Slug,
                SeasonNumber = episode.Season.Number
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEpisodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var episode = await _episodeRepository.GetEpisodeWithRelatedData(model.Id);

            if (episode == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Season with Id = {model.Id} cannot be found";
                return View("NotFound");
            }

            episode.Number = model.Number;
            episode.Name = model.Name;

            await _episodeRepository.Update(episode);

            return RedirectToAction("Details", new { seriesSlug = model.SeriesSlug, seasonNumber = model.SeasonNumber, episodeNumber = model.Number });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var episode = await _episodeRepository.GetEpisodeWithRelatedData(id);

            if (episode == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Season with Id = {id} cannot be found";
                return View("NotFound");
            }

            await _episodeRepository.Delete(episode);

            return RedirectToAction("Details", "Season", new { seriesSlug = episode.Season.Series.Slug, number = episode.Season.Number });
        }
    }
}
