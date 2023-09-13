using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MovieController(IMovieRepository movieRepository, UserManager<ApplicationUser> userManager,
            IWebHostEnvironment hostEnvironment)
        {
            _movieRepository = movieRepository;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await _movieRepository.GetAllMovies();
            var model = allMovies.Select(movie => new CardViewModel { Name = movie.Name, Slug = movie.Slug, PhotoPath = PosterViewModel.GetPhoto(movie.PhotoPath) }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
