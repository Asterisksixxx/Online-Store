using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Online_Store.Data;
using Online_Store.Models;
using Online_Store.Services;
using Online_Store.ViewModels;

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
        public async Task<IActionResult> Details(string login)
        {
            var user = await _userService.GetAsyncOne(login);
            EditUserViewModel userEdit = new EditUserViewModel
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                DataBorn = user.DataBorn,
                Number = user.Number,
                Id = user.Id,
            };
            return  View(userEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Details(EditUserViewModel userEdit)
        {
            await _userService.Update(userEdit);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Login(User use)
        {
            var user = await _userService.GetOne(use.Login,use.Password);
            if (user != null)
            {
               await Authenticate(user);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Введены неверные данные");
            return View(use);
        }
        public async Task Authenticate(User user)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(await _userService.Authenticate(user)));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
    }
}
