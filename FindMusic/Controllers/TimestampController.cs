using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels;
using FindMusic.ViewModels.TimestampViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    public class TimestampController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITimestampRepository _timestampRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IMovieRepository _movieRepository;

        public TimestampController(UserManager<ApplicationUser> userManager, ITimestampRepository timestampRepository,
            IEpisodeRepository episodeRepository, IMovieRepository movieRepository)
        {
            _userManager = userManager;
            _timestampRepository = timestampRepository;
            _episodeRepository = episodeRepository;
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id, string type)
        {
            if (type == "episode")
            {
                var episode = await _episodeRepository.GetEpisodeWithRelatedData(id);

                if (episode == null)
                {
                    Response.StatusCode = 404;
                    ViewBag.ErrorMessage = $"Episode with Id = {id} cannot be found";
                    return View("NotFound");
                }

                var model = new CreateTimestampViewModel
                {
                    Content = new ContentDetailsViewModel
                    {
                        Episode = new RelatedEpisodeViewModel
                        {
                            Number = episode.Number,
                            Season = episode.Season.Number,
                            SeriesName = episode.Season.Series.Name,
                            SeriesSlug = episode.Season.Series.Slug
                        }
                    }
                };

                return View(model);

            }

            else if (type == "movie")
            {
                var movie = await _movieRepository.GetMovieById(id);

                if (movie == null)
                {
                    Response.StatusCode = 404;
                    ViewBag.ErrorMessage = $"Movie with Id = {id} cannot be found";
                    return View("NotFound");
                }

                var model = new CreateTimestampViewModel
                {
                    Content = new ContentDetailsViewModel
                    {
                        Movie = new RelatedMovieViewModel
                        {
                            Name = movie.Name,
                            Slug = movie.Slug
                        }
                    }
                };

                return View(model);
            }

            Response.StatusCode = 404;
            ViewBag.ErrorMessage = $"{type} cannot be found";
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTimestampViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var content = model.Content;

            if (content.Episode != null && content.Movie == null)
            {
                var episode = await _episodeRepository.GetEpisodeWithRelatedData(content.Episode.Number, content.Episode.Season, content.Episode.SeriesSlug);

                if (episode == null)
                {
                    Response.StatusCode = 404;
                    ViewBag.ErrorMessage = $"Episode cannot be found";
                    return View("NotFound");
                }

                var timestamp = new Timestamp
                {
                    Hour = model.Hour,
                    Minute = model.Minute,
                    Second = model.Second,
                    User = await _userManager.GetUserAsync(User),
                    Episode = episode,
                };

                await _timestampRepository.Add(timestamp);

                var routeValues = new
                {
                    seriesSlug = episode.Season.Series.Slug,
                    seasonNumber = episode.Season.Number,
                    episodeNumber = episode.Number
                };

                return RedirectToAction("Details", "Episode", routeValues);
            }

            else if (content.Movie != null && content.Episode == null)
            {
                var movie = await _movieRepository.GetMovieBySlug(content.Movie.Slug);

                if (movie == null)
                {
                    Response.StatusCode = 404;
                    ViewBag.ErrorMessage = $"Movie like {content.Movie.Slug} cannot be found";
                    return View("NotFound");
                }

                var timestamp = new Timestamp
                {
                    Hour = model.Hour,
                    Minute = model.Minute,
                    Second = model.Second,
                    User = await _userManager.GetUserAsync(User),
                    Movie = movie,
                };

                await _timestampRepository.Add(timestamp);

                return RedirectToAction("Details", "Movie", new { slug = movie.Slug });
            }

            Response.StatusCode = 404;
            ViewBag.ErrorMessage = "Issues with content!";
            return View("NotFound");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var timestamp = await _timestampRepository.GetTimestampWithRelatedData(id);
            if (timestamp == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Timestamp with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new UpdateTimestampViewModel
            {
                Id = timestamp.Id,
                Hour = timestamp.Hour,
                Minute = timestamp.Minute,
                Second = timestamp.Second
            };


            if (timestamp.Episode != null && timestamp.Movie == null)
            {
                var episode = timestamp.Episode;

                model.Content = new ContentDetailsViewModel
                {
                    Episode = new RelatedEpisodeViewModel
                    {
                        Number = episode.Number,
                        Season = episode.Season.Number,
                        SeriesName = episode.Season.Series.Name,
                        SeriesSlug = episode.Season.Series.Slug
                    }
                };

                return View(model);
            }

            else if (timestamp.Movie != null && timestamp.Episode == null)
            {
                var movie = timestamp.Movie;

                model.Content = new ContentDetailsViewModel
                {
                    Movie = new RelatedMovieViewModel
                    {
                        Name = movie.Name,
                        Slug = movie.Slug
                    }
                };

                return View(model);
            }

            Response.StatusCode = 404;
            ViewBag.ErrorMessage = "Issues with content!";
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTimestampViewModel model)
        {
            var timestamp = await _timestampRepository.GetTimestampWithRelatedData(model.Id);
            if (timestamp == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Timestamp with Id = {model.Id} cannot be found";
                return View("NotFound");
            }

            if (timestamp.Episode == null && timestamp.Movie == null || timestamp.Episode != null && timestamp.Movie != null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Related content cannot be found";
                return View("NotFound");
            }

            timestamp.Hour = model.Hour;
            timestamp.Minute = model.Minute;
            timestamp.Second = model.Second;
            await _timestampRepository.Update(timestamp);

            if (timestamp.Episode != null)
            {
                var episode = timestamp.Episode;

                var routeValues = new
                {
                    seriesSlug = episode.Season.Series.Slug,
                    seasonNumber = episode.Season.Number,
                    episodeNumber = episode.Number
                };

                return RedirectToAction("Details", "Episode", routeValues);
            }

            return RedirectToAction("Details", "Movie", new { slug = timestamp.Movie?.Slug });
        }
    }
}
