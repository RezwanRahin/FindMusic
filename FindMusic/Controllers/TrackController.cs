using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels;
using FindMusic.ViewModels.TrackViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    public class TrackController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITrackRepository _trackRepository;
        private readonly ITimestampRepository _timestampRepository;

        public TrackController(UserManager<ApplicationUser> userManager, ITrackRepository trackRepository,
            ITimestampRepository timestampRepository)
        {
            _userManager = userManager;
            _trackRepository = trackRepository;
            _timestampRepository = timestampRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int timestampId)
        {
            var timestamp = await _timestampRepository.GetTimestampWithRelatedData(timestampId);

            if (timestamp == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Timestamp of Id = {timestampId} cannot be found";
                return View("NotFound");
            }

            var model = new CreateTrackViewModel
            {
                Timestamp = new RelatedTimestampViewModel
                {
                    Id = timestamp.Id,
                    Hour = timestamp.Hour,
                    Minute = timestamp.Minute,
                    Second = timestamp.Second
                }
            };

            if (timestamp.Episode != null)
            {
                model.Content = new ContentDetailsViewModel
                {
                    Episode = new RelatedEpisodeViewModel
                    {
                        Number = timestamp.Episode.Number,
                        Season = timestamp.Episode.Season.Number,
                        SeriesName = timestamp.Episode.Season.Series.Name,
                        SeriesSlug = timestamp.Episode.Season.Series.Slug
                    }
                };

                return View(model);
            }

            else if (timestamp.Movie != null)
            {
                model.Content = new ContentDetailsViewModel
                {
                    Movie = new RelatedMovieViewModel
                    {
                        Name = timestamp.Movie.Name,
                        Slug = timestamp.Movie.Slug
                    }
                };

                return View(model);
            }

            Response.StatusCode = 404;
            ViewBag.ErrorMessage = "Issue with content";
            return View("NotFound");
        }
    }
}
