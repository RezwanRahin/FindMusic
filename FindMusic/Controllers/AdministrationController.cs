using FindMusic.Models;
using FindMusic.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
	public class AdministrationController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AdministrationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ListUsers()
		{
			var users = _userManager.Users;
			return View(users);
		}

		[HttpGet]
		public async Task<IActionResult> EditUser(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
			{
				Response.StatusCode = 404;
				ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
				return View("NotFound");
			}

			var userClaims = await _userManager.GetClaimsAsync(user);
			var userRoles = await _userManager.GetRolesAsync(user);

			var model = new EditUserViewModel
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				UserName = user.UserName,
				Gender = user.Gender,
				DOB = user.DOB,
				Claims = userClaims.Select(c => c.Value).ToList(),
				Roles = userRoles
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> EditUser(EditUserViewModel model)
		{
			var user = await _userManager.FindByIdAsync(model.Id);

			if (user == null)
			{
				Response.StatusCode = 404;
				ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
				return View("NotFound");
			}

			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.Email = model.Email;
			user.UserName = model.UserName;
			user.Gender = model.Gender;
			user.DOB = model.DOB;

			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded)
			{
				return RedirectToAction("ListUsers");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
			{
				Response.StatusCode = 404;
				ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
				return View("NotFound");
			}

			var result = await _userManager.DeleteAsync(user);

			if (result.Succeeded)
			{
				return RedirectToAction("ListUsers");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View("ListUsers");
		}
	}
}
