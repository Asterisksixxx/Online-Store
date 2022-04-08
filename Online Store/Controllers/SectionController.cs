using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Store.Services;
using System.Threading.Tasks;
using Online_Store.Models;
using Online_Store.ViewModels;

namespace Online_Store.Controllers
{
    public class SectionController : Controller
    {
        private readonly ISectionService _sectionService;
        private readonly ISubSectionService _subSectionService;

        public SectionController(ISectionService sectionService, ISubSectionService subSectionService)
        {
            _sectionService = sectionService;
            _subSectionService = subSectionService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _sectionService.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Section section)
        {
            await _sectionService.CreateAsync(section);
            return RedirectToAction("Index", "Section");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSection(Guid id)
        {
            return View(await _sectionService.GetOneAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSection(Section section)
        {
            await _sectionService.UpdateAsync(section);
            return RedirectToAction("Index", "Section");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatusSection()
        {
            await _sectionService.UpdateStatus();
            return RedirectToAction("Index", "Section");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteSection(Section section, Guid id)
        {
            section = await _sectionService.GetOneAsync(id);
            var deleteVm = await _subSectionService.GetAllFromSection(section);
            ViewBag.SubSections = new SelectList(deleteVm.SubSections, "Id", "Name");
            return View(deleteVm);
        }

        [HttpPost]
        public  async Task<IActionResult>  DeleteSection(Guid id)
        {
           await _sectionService.Delete(id);
            return RedirectToAction("Index", "Section");
        }
        
    }
}
