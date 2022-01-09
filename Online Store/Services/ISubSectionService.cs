using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;

namespace Online_Store.Services
{
    public interface ISubSectionService
    {
       Task<IEnumerable<SubSection>> GetAllAsync();
        Task<SubSection> GetOneAsync(Guid id);
        Task CreateAsync(SubSection subSection);
        Task Delete(Guid id);
        Task Update(SubSection subSection);
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
            return await _appDataContext.SubSections.ToListAsync();
        }

        public async Task<SubSection> GetOneAsync(Guid id)
        {
          return await _appDataContext.SubSections.FirstOrDefaultAsync(section => section.Id == id);
        }

        public async Task CreateAsync(SubSection subSection)
        {
           await _appDataContext.SubSections.AddAsync(subSection);
            await _appDataContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _appDataContext.SubSections.Remove(_appDataContext.SubSections.FirstOrDefault(section => section.Id == id));
           await _appDataContext.SaveChangesAsync();
        }

        public async Task Update(SubSection subSection)
        {
            _appDataContext.SubSections.Update(subSection);
            await _appDataContext.SaveChangesAsync();
        }
    }
}
