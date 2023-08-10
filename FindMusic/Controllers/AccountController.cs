using FindMusic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
    [Route("Account/[action]/{username?}")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null ? Json(true) : Json($"Email {email} is already in use");
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public Task<IActionResult> IsEmailOfOther(string email, string id)
        {
            var result = _userManager.Users.Any(u => u.Email == email && u.Id != id);
            return Task.FromResult<IActionResult>(result ? Json($"Email {email} is already in use") : Json(true));
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsUsernameInUse(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user == null ? Json(true) : Json($"Username '{username}' is already in use");
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public Task<IActionResult> IsUsernameOfOther(string username, string id)
        {
            var result = _userManager.Users.Any(u => u.UserName == username && u.Id != id);
            return Task.FromResult<IActionResult>(result ? Json($"Username '{username}' is already in use") : Json(true));
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
