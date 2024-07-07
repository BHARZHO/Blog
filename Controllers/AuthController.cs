using AspNetCoreHero.ToastNotification.Abstractions;
using BlogApplication.Models;
using BlogApplication.ViewModels.AuthViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class AuthController(SignInManager<User> signInManager, UserManager<User> userManager, INotyfService notyf
) : Controller
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly INotyfService _notyfService = notyf;


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Username) ?? await _userManager.FindByEmailAsync(model.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user!.UserName!, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _notyfService.Success("Login succesful");
                    return RedirectToAction("Index", "Admin");
                }
            }

            ModelState.AddModelError("", "Invalid login attempt");
            _notyfService.Error("Invalid login attempt");
            return View(model);
        }


        return View(model);
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == model.Email || u.UserName == model.Username);

            if (existingUser != null)
            {
                _notyfService.Warning("User already exist!");
                return View();
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var message = string.Join(",",result.Errors.Select(e => e.Description));
                _notyfService.Error(message);
                return View();
            }

            _notyfService.Success("Registration was successful");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Login", "Auth");
        }
        return View(model);
    }



    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "home");
    }
}
