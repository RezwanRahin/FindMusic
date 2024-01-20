using FindMusic.Extensions;
using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using FindMusic.ViewModels.MovieViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FindMusic.ViewModels.TimestampViewModels;
using FindMusic.ViewModels.TrackViewModels;

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

            return RedirectToAction("Details", new { slug = movie.Slug });
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

                Timestamps = new List<TimestampDetailsViewModel>()
            };

            foreach (var timestamp in movie.Timestamps)
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
        [Route("[controller]/[action]/{slug?}")]
        public async Task<IActionResult> Update(string slug)
        {
            var movie = await _movieRepository.GetMovieBySlug(slug);

            if (movie == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Movie like {slug} cannot be found";
                return View("NotFound");
            }

            var model = new UpdateMovieViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Slug = movie.Slug,
                ReleaseDate = movie.ReleaseDate,
                PhotoPath = movie.PhotoPath
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var movie = await _movieRepository.GetMovieById(model.Id);

            if (movie == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Movie {model.Name} cannot be found";
                return View("NotFound");
            }

            movie.Name = model.Name;
            movie.Slug = model.Name.Slugify();
            movie.ReleaseDate = model.ReleaseDate;

            if (model.Photo != null)
            {
                movie.PhotoPath = model.Photo.ProcessUploadedFile(_hostEnvironment);
                model.PhotoPath?.DeleteImageFile(_hostEnvironment);
            }

            await _movieRepository.Update(movie);

            return RedirectToAction("Details", new { slug = movie.Slug });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);

            if (movie != null)
            {
                var photoPath = movie.PhotoPath;
                await _movieRepository.Delete(movie);
                photoPath.DeleteImageFile(_hostEnvironment);
            }

            return RedirectToAction("Index");
        }
    }
}
