using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Online_Store.Services;

namespace Online_Store.Controllers
{
    public class SectionController : Controller
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _sectionService.GetAllAsync());
        }
        [HttpGet]
        public  IActionResult Create()
        {
            //ViewBag.ListSubSection = new SelectList();
            return View();
        }

    }
}
