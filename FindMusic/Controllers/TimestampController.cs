using FindMusic.Models;
using FindMusic.Repository;
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
    }
}
