using FindMusic.Models;
using FindMusic.Repository;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
