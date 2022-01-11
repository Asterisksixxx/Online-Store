using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;
using Online_Store.ViewModels;

namespace Online_Store.Services
{
    public interface ISubSectionService
    {
       Task<IEnumerable<SubSection>> GetAllAsync();
       Task<IEnumerable<SubSection>> GetAllAsyncForProduct();
        Task<SubSection> GetOneAsync(Guid id);
        Task CreateAsync(SubSection subSection);
        Task Delete(Guid id);
        Task Update(SubSection subSection);
        Task<DeleteSectionViewModel> GetAllFromSection(Section section);
        Task UpdateStatus();
    }

    public class SubSectionService : ISubSectionService
    {
        private readonly AppDataContext _appDataContext;

        public SubSectionService(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async Task<IEnumerable<SubSection>> GetAllAsync()
        {
            var listSubsections = _appDataContext.SubSections.AsNoTracking().
                Include(section => section.Section);
            listSubsections.Include(section => section.Product);
            await listSubsections.ToListAsync();
            return listSubsections;
        }

        public async Task<IEnumerable<SubSection>> GetAllAsyncForProduct()
        {
            return await _appDataContext.SubSections.ToListAsync();
        }

        public async Task<SubSection> GetOneAsync(Guid id)
        { var subSections=_appDataContext.SubSections.Include(section =>section.Section);
          return await subSections.FirstOrDefaultAsync(section => section.Id == id);
        }

        public async Task CreateAsync(SubSection subSection)
        {
           await _appDataContext.SubSections.AddAsync(subSection);
            await _appDataContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
          var listSubsect= _appDataContext.SubSections.Include(section => section.Product);
          var sub = await listSubsect.FirstOrDefaultAsync(section => section.Id == id);
          _appDataContext.SubSections.Remove(sub);
           await _appDataContext.SaveChangesAsync();
        }

        public async Task Update(SubSection subSection)
        {
            var sections = _appDataContext.SubSections.AsNoTracking().Include(section1 => section1.Product).ToList();
            var mainSection = sections.FirstOrDefault(section1 => section1.Id == subSection.Id);
            if (mainSection != null) mainSection.ProductCount =Convert.ToUInt32(mainSection.Product.Count);
            if (mainSection != null) subSection.ProductCount = mainSection.ProductCount;
            _appDataContext.SubSections.Update(subSection);
            await _appDataContext.SaveChangesAsync();
        }

        public async Task<DeleteSectionViewModel> GetAllFromSection(Section section)
        {
            var subSections =  _appDataContext.SubSections.
                Include(subSection => subSection.Section);
          
            return new DeleteSectionViewModel
            {
                Section = section,
                SubSections = await (subSections.Where(subSection => subSection.SectionId == section.Id)).ToListAsync()
            };
        }
        public async Task UpdateStatus()
        {
            var sec = await _appDataContext.SubSections.ToListAsync();
            foreach (var section in sec)
            {
                await Update(section);
            }
        }
    }
}
