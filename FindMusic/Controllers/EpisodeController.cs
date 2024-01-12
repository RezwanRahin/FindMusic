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

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> DoesNumberExist(string seriesSlug, int seasonNumber, int episodeNumber)
        {
            var episode = await _episodeRepository.GetEpisode(episodeNumber, seasonNumber, seriesSlug);
            return episode == null ? Json(true) : Json($"Episode with Number = {episodeNumber} already exists!");
        }
    }
}
