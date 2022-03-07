using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Online_Store.Models;
using Online_Store.Services;

namespace Online_Store.Controllers
{
    public class NotConfirmUserController : Controller
    {
        private readonly INotConfirmUserService _notConfirmUserService;
        private readonly IUserService _userService;
        private readonly IBasketService _basketService;

        public NotConfirmUserController(INotConfirmUserService notConfirmUserService, IUserService userService, IBasketService basketService)
        {
            _notConfirmUserService = notConfirmUserService;
            _userService = userService;
            _basketService = basketService;
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(NotConfirmUser notConfirmUser)
        {

            if (!ModelState.IsValid) return View();
            if (!_notConfirmUserService.Check(notConfirmUser.Name, notConfirmUser.Login))
            {
                await _notConfirmUserService.CreateAsync(notConfirmUser);
                return RedirectToAction("Confirm", "NotConfirmUser", notConfirmUser);
            }
            else
                ModelState.AddModelError("", "Пользователь с такими данными уже существует");

            return View(notConfirmUser);
        }


        public IActionResult Confirm(NotConfirmUser notConfirmUser)
        {
            return View(notConfirmUser);
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(string code, NotConfirmUser notConfirmUser)
        {
            if (notConfirmUser.Code == code)
            {
                await _userService.CreateAsync(notConfirmUser);
                await _basketService.CreateAsync(notConfirmUser.Id);
                return RedirectToAction("Login", "User");
            }
            else
            {
                IEmailService mailService = new EmailService();
                await mailService.SendEmailAsync(notConfirmUser.Email, "Подтвердите свою почту",
                    notConfirmUser.Code);
                return RedirectToAction("Confirm");
            }
        }
    }
}
