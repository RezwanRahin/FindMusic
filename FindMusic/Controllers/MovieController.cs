using FindMusic.Extensions;
using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using FindMusic.ViewModels.MovieViewModels;
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            var movie = new Movie
            {
                Name = model.Name,
                ReleaseDate = model.ReleaseDate,
                Slug = model.Name.Slugify(),
                PhotoPath = model.Photo.ProcessUploadedFile(_hostEnvironment),
                User = user,
            };

            await _movieRepository.Add(movie);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/[action]/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            var movie = await _movieRepository.GetMovieWithRelatedData(slug);

            if (movie == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Movie like {slug} cannot be found";
                return View("NotFound");
            }

            var model = new MovieDetailsViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Slug = movie.Slug,
                ReleaseDate = movie.ReleaseDate,
                Poster = new PosterViewModel(movie.PhotoPath),
                Contributor = new ContributorViewModel(movie.User),
            };

            return View(model);
        }
    }
}
