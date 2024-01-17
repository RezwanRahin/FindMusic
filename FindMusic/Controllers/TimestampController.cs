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
    }
}
