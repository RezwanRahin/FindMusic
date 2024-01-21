using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FindMusic.Models;
using Microsoft.AspNetCore.Authorization;
using FindMusic.Repository;
using FindMusic.ViewModels;

namespace FindMusic.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly ISeriesRepository _seriesRepository;

        public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository, ISeriesRepository seriesRepository)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _seriesRepository = seriesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieRepository.GetLatestMovies();
            var series = await _seriesRepository.GetLatestSeries();

            var model = new LatestContentViewModel
            {
                Movies = new List<CardViewModel>(),
                Series = new List<CardViewModel>()
            };

            foreach (var movie in movies)
            {
                var card = new CardViewModel
                {
                    Name = movie.Name,
                    Slug = movie.Slug,
                    PhotoPath = PosterViewModel.GetPhoto(movie.PhotoPath)
                };
                model.Movies.Add(card);
            }

            foreach (var s in series)
            {
                var card = new CardViewModel
                {
                    Name = s.Name,
                    Slug = s.Slug,
                    PhotoPath = PosterViewModel.GetPhoto(s.PhotoPath)
                };
                model.Series.Add(card);
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
