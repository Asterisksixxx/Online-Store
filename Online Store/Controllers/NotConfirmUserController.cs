using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Online_Store.Models;
using Online_Store.Services;

namespace Online_Store.Controllers
{
    public class NotConfirmUserController : Controller
    {
        private readonly INotConfirmUserService _notConfirmUserService;
        private readonly IUserService _userService;

        public NotConfirmUserController(INotConfirmUserService notConfirmUserService, IUserService userService)
        {
            _notConfirmUserService = notConfirmUserService;
            _userService = userService;
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
                ModelState.AddModelError("", "The user already exists");

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
                return RedirectToAction("Login", "User");
            }
            else
            {
                EmailService mailService = new EmailService();
                await mailService.SendEmailAsync(notConfirmUser.Email.ToString(), "Confirm Account and Email",
                    notConfirmUser.Code);
                return RedirectToAction("Confirm");
            }
        }
    }
}
