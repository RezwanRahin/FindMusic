﻿using FindMusic.Models;
using FindMusic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMusic.Controllers
{
	[Authorize(Roles = "Admin")]
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

		[HttpGet]
		public IActionResult CreateRole()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var identityRole = new IdentityRole { Name = model.RoleName };
			var result = await _roleManager.CreateAsync(identityRole);

			if (result.Succeeded)
			{
				return RedirectToAction("ListRoles");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult ListRoles()
		{
			var roles = _roleManager.Roles;
			return View(roles);
		}

		[HttpGet]
		public async Task<IActionResult> EditRole(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);

			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
				return View("NotFound");
			}

			var model = new EditRoleViewModel { Id = role.Id, RoleName = role.Name };

			foreach (var user in _userManager.Users.ToList())
			{
				if (await _userManager.IsInRoleAsync(user, role.Name))
				{
					model.Users.Add(user.UserName);
				}
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> EditRole(EditRoleViewModel model)
		{
			var role = await _roleManager.FindByIdAsync(model.Id);

			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
				return View("NotFound");
			}

			role.Name = model.RoleName;
			var result = await _roleManager.UpdateAsync(role);

			if (result.Succeeded)
			{
				return RedirectToAction("ListRoles");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteRole(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);

			if (role == null)
			{
				Response.StatusCode = 404;
				ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
				return View("NotFound");
			}

			var result = await _roleManager.DeleteAsync(role);

			if (result.Succeeded)
			{
				return RedirectToAction("ListRoles");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View("ListRoles");
		}

		[HttpGet]
		public async Task<IActionResult> EditUsersInRole(string roleId)
		{
			ViewBag.roleId = roleId;

			var role = await _roleManager.FindByIdAsync(roleId);

			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
				return View("NotFound");
			}

			var model = new List<UserRoleViewModel>();

			foreach (var user in _userManager.Users.ToList())
			{
				var userRoleViewModel = new UserRoleViewModel
				{
					UserId = user.Id,
					UserName = user.UserName,
					IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
				};

				model.Add(userRoleViewModel);
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);

			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
				return View("NotFound");
			}

			if (model.Any())
			{
				for (int i = 0; i < model.Count; i++)
				{
					var user = await _userManager.FindByIdAsync(model[i].UserId);
					IdentityResult result = null;

					if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
					{
						result = await _userManager.AddToRoleAsync(user, role.Name);
					}
					else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
					{
						result = await _userManager.RemoveFromRoleAsync(user, role.Name);
					}

					if (result == null || result.Succeeded) continue;
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return RedirectToAction("EditUsersInRole", new { Id = roleId });
				}
			}

			return RedirectToAction("EditRole", new { Id = roleId });
		}
	}
}
