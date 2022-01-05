using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Online_Store.Data;
using Online_Store.Models;
using Online_Store.Services;

namespace Online_Store.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly AppDataContext _appDataContext;
        public UserController(IUserService userService, AppDataContext appDataContext)
        {
            _userService = userService;
            _appDataContext = appDataContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Login(User use)
        {
            var user = _userService.GetAsyncOne(use.Email,use.Password);
            if (user != null)
            {
               await Authenticate(user);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return View(use);
        }
        public async  Task Authenticate(User user)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(_userService.Authenticate(user)));
        }
        public async Task<IActionResult> Logout(User user)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
    }
}
