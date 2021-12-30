﻿using Microsoft.EntityFrameworkCore;
using Online_Store.Models;

namespace Online_Store.Data
{
    public class AppDataContext:DbContext
    {
       
        public AppDataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SubSection> SubSections { get; set; }
    }
}
