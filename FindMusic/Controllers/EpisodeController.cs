﻿using FindMusic.Models;
using FindMusic.Repository;
using FindMusic.ViewModels.EpisodeViewModels;
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

        [HttpGet]
        public async Task<IActionResult> Create(string seriesSlug, int seasonNumber)
        {
            var season = await _seasonRepository.GetSeason(seasonNumber, seriesSlug);

            if (season == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"Season with Number = {seasonNumber} & Series like {seriesSlug} cannot be found";
                return View("NotFound");
            }

            var model = new CreateEpisodeViewModel
            {
                SeriesName = season.Series.Name,
                SeriesSlug = season.Series.Slug,
                SeasonNumber = season.Number
            };

            return View(model);
        }
    }
}
