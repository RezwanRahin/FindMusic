using FindMusic.Models;
using FindMusic.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeasonController(ISeriesRepository seriesRepository, ISeasonRepository seasonRepository,
            UserManager<ApplicationUser> userManager)
        {
            _seriesRepository = seriesRepository;
            _seasonRepository = seasonRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> DoesNumberExist(string seriesSlug, int number)
        {
            var season = await _seasonRepository.GetSeason(number, seriesSlug);
            return season == null ? Json(true) : Json($"Season with Number = {number} already exists!");
        }
    }
}
