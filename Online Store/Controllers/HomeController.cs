using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Online_Store.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Online_Store.Services;

namespace Online_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }


        public async Task<IActionResult> Index(Guid id)
        {
            return View(await _homeService.GetAllData(id));
        }
        [HttpGet]
        public IActionResult FAQ()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AboutUS()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
