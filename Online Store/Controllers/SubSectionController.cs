using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Store.Models;
using Online_Store.Services;
using Online_Store.ViewModels;

namespace Online_Store.Controllers
{
    public class SubSectionController : Controller
    {
        private readonly ISubSectionService _subSectionService;
        private readonly ISectionService _sectionService;

        public SubSectionController(ISubSectionService subSectionService, ISectionService sectionService)
        {
            _subSectionService = subSectionService;
            _sectionService = sectionService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _subSectionService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateSubSection createSubSection = new CreateSubSection {Sections = (await _sectionService.GetAllAsync()).ToList()};
            ViewBag.Sections = new SelectList(createSubSection.Sections, "Id", "Name");
            return View(createSubSection);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubSection subSection)
        {
            await _subSectionService.CreateAsync(subSection);
            return RedirectToAction("Index", "SubSection");
        }
    }
}
