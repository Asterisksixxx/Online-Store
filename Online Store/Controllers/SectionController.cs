using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> Index()
        {
            return View(await _sectionService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

    }
}
