using FindMusic.Models;
using FindMusic.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    public class EpisodeController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public EpisodeController(ISeasonRepository seasonRepository, IEpisodeRepository episodeRepository,
            UserManager<ApplicationUser> userManager)
        {
            _seasonRepository = seasonRepository;
            _episodeRepository = episodeRepository;
            _userManager = userManager;
        }
    }
}
