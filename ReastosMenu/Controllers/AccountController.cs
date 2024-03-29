﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReastosMenu.Entities.Identity;
using ReastosMenu.Models;
using System.Security.Claims;

namespace ReastosMenu.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        public AccountController(SignInManager<ApplicationUser> signInManager, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;

        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()

        {

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }


            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user != null)
            {
                // try to sign in the user with the credentials they provided
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var firstNameClaim = new Claim("FirstName", user.FirstName);
                    await _userManager.AddClaimAsync(user, firstNameClaim);

                    var lastNameClaim = new Claim("LastName", user.LastName);
                    await _userManager.AddClaimAsync(user, lastNameClaim);


                    await _signInManager.RefreshSignInAsync(user);


                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Any(x => x == "Admin"))
                    {
                        return Redirect(Url.Action("Index", "Admin"));
                    }

                    //else if (roles.Any(x => x == "Visitor"))
                    //{
                    //    return Redirect(Url.Action("Index", "Home"));
                    //}

                    return Redirect(GetRedirectUrl(returnUrl));

                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid userName or password.");
                    return View(loginModel);

                }
            }


            return View(loginModel);

        }

        public string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Admin");
            }

            return returnUrl;
        }

        private void AddModelErrors(IdentityResult? result)
        {
            if (result.Errors.Any())
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

            }

        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
