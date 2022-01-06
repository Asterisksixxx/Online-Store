using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;

namespace Online_Store.Services
{
    public interface IProductService
    {
      Task<IEnumerable<Product>>  GetAsync();
       Task<Product> GetAsync(Guid id);
        Task CreateAsync(Product product);
        void Delete(Guid id);
        Task Update(Product product);
    }

    public class ProductService : IProductService
    {
        private readonly AppDataContext _appDataContext;

        public ProductService(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async Task<IEnumerable<Product>>  GetAsync()
        {
            return await _appDataContext.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _appDataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            
        }

        public async Task CreateAsync(Product product)
        {
           await _appDataContext.Products.AddAsync(product);
          await _appDataContext.SaveChangesAsync();
        }

        public void Delete(Guid id)
        {
            _appDataContext.Products.Remove(_appDataContext.Products.FirstOrDefault(product => product.Id == id));
            _appDataContext.SaveChanges();
        }

        public Task Update(Product product)
        {
            _appDataContext.Products.Update(product);
            return _appDataContext.SaveChangesAsync();
        }
    }
}
