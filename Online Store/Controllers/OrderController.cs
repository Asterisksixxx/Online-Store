using Microsoft.AspNetCore.Mvc;

namespace Online_Store.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
