﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;

namespace Online_Store.Services
{
    public interface ISectionService
    {
        Task<IEnumerable<Section>> GetAllAsync();
        Task<Section> GetOneAsync(Guid id);
        Task CreateAsync(Section section);
    }

    public class SectionService:ISectionService
    {
        private readonly AppDataContext _appDataContext;

        public SectionService(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async Task<IEnumerable<Section>> GetAllAsync()
        {
          return await _appDataContext.Sections.ToListAsync();
        }

        public async Task<Section> GetOneAsync(Guid id)
        {
            return await _appDataContext.Sections.FirstOrDefaultAsync(section => section.Id == id);
        }

        public async Task CreateAsync(Section section)
        {
            await _appDataContext.Sections.AddAsync(section);
            await _appDataContext.SaveChangesAsync();
        }
    }
}