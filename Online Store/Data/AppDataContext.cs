using System;
using Microsoft.EntityFrameworkCore;
using Online_Store.Models;

namespace Online_Store.Data
{
    public class AppDataContext:DbContext
    {
       
        public AppDataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roleUser = new Role
            {
                Id = Guid.Parse("9DC6E38B-1687-4E74-A4C7-D2ACBECFE6FC"),
                Name = "USER"
            };
            var roleGuest = new Role
            {
                Id = Guid.Parse("B4E0CA41-0F9B-4A08-B42B-8024B4CE46C7"),
                Name = "GUEST"
            };
            var adminRoleId = Guid.Parse("7645E9B7-F9ED-460D-997A-BDA7AF4C9F8B");
            var roleAdmin = new Role
            {
                Id = adminRoleId,
                Name = "ADMIN"
            };
         var admin=new User
         {
             Id = Guid.Parse("088075C9-5A9B-4583-B0E4-279886D46A5D"),
             Email = "admin@admin.by",
             DataBorn = DateTime.Now,
             Name = "admin",
             Surname = "admin",
             Login = "admin",
             Password = "admin",
             Number ="+37500000000",
             RoleId = roleAdmin.Id,
         };
           
           modelBuilder.Entity<Role>().HasData(roleUser, roleGuest, roleAdmin);
           modelBuilder.Entity<User>().HasData(admin);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SubSection> SubSections { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<NotConfirmUser> NotConfirmUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
    }
}
