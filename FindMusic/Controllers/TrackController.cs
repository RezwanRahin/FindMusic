using FindMusic.Models;
using FindMusic.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    public class TrackController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITrackRepository _trackRepository;
        private readonly ITimestampRepository _timestampRepository;

        public TrackController(UserManager<ApplicationUser> userManager, ITrackRepository trackRepository,
            ITimestampRepository timestampRepository)
        {
            _userManager = userManager;
            _trackRepository = trackRepository;
            _timestampRepository = timestampRepository;
        }
    }
}
