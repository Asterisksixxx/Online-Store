using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Services;

namespace Online_Store.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string login)
        {
            return View(await _basketService.GetOneAsync(login));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Guid productId,string login,uint count)
        {
            await _basketService.UpdateAsync(productId,login,count);
            return RedirectToAction("Details","Product",routeValues:new{ productId});
        }

        [HttpPost]
        public async Task<IActionResult> RemoveProduct(Guid basketProductId,uint count, Guid basketId)
        {
            var login = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            await _basketService.RemoveBasketProduct(basketProductId, count,basketId);
            return RedirectToAction("Index", "Basket",
                routeValues: new {login});
        }
    }
}
