using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FindMusic.Models;
using Microsoft.AspNetCore.Authorization;
using FindMusic.Repository;

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

        public IActionResult Index()
        {
            return View();
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
