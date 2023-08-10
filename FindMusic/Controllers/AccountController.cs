using FindMusic.Models;
using FindMusic.ViewModels;
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                Gender = model.Gender,
                DOB = model.DOB
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Identifier) ?? await _userManager.FindByEmailAsync(model.Identifier);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != await _userManager.GetUserAsync(User) && !User.IsInRole("Admin"))
            {
                return RedirectToAction("AccessDenied");
            }

            if (user == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"User with Username = {username} cannot be found";
                return View("NotFound");
            }

            var model = new UserDetailsViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Gender = user.Gender,
                DOB = user.DOB,
                PhotoPath = user.PhotoPath
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = $"User with Username = {username} cannot be found";
                return View("NotFound");
            }

            var model = new EditProfileViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Gender = user.Gender,
                DOB = user.DOB,
                PhotoPath = user.PhotoPath
            };

            return View(model);
        }
    }
}
