using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Base;
using Presentation.Models;
using Business.Services.Base;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMyEmailSender _myEmailSender;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IMyEmailSender myEmailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _myEmailSender = myEmailSender;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                await UpdateUserCookie(user);

                PopUpSuccess("Your email has been confirmed");
                return View("ConfirmEmail"); 
            }
            else
            {
                PopUpError("Error in email confirmation");
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendConfirmationEmail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme);

                await _myEmailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your email by clicking here: <a href='{confirmationLink}'>link</a>");

                PopUpInfo("Confirmation email resent. Please check your inbox.");
                return RedirectToAction("Profile");
            }

            PopUpInfo("Your email has already been confirmed");
            return RedirectToAction("Profile");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && user.UserName != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        PopUpInfo($"{User.Identity?.Name} logged in");
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                    return View(model);
                }
            }

            ModelState.AddModelError(string.Empty, "Error, password or email is not the default");
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity?.Name;

            await _signInManager.SignOutAsync();
            PopUpInfo($"User {username} successfully logged out");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                    await _myEmailSender.SendEmailAsync(model.Email, "Confirm your email",
                        $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");

                    PopUpSuccess($"User {user.UserName} created successfully. Please confirm your email before logging in.");
                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError(string.Empty, "Error in registration");
            }
            return View(model);
        }

        [Authorize (Roles = "Admin")]
        [HttpGet]
        public IActionResult AdminArea()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminArea(UserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found.");
                    return View(model);
                }

                var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
                if (!roleExists)
                {
                    ModelState.AddModelError("", "Role don't exist.");
                    return View(model);
                }

                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Error when removing existing roles.");
                        return View(model);
                    }
                }

                var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                if (result.Succeeded)
                {
                    if (model.RoleName != "User")
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    PopUpSuccess($"Role {model.RoleName} successfully assigned to user {model.Username}");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Error when assigning role.");
                }
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            await UpdateUserCookie(user);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new UserProfileViewModel
            {
                Username = user.UserName,
                Email = user.Email
            };

            return View(model);
        }

        private async Task UpdateUserCookie(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120) 
            };

            await _signInManager.SignInWithClaimsAsync(user, authProperties, claims);
        }
    }
}
