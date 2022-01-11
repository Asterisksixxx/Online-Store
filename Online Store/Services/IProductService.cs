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
      Task<IEnumerable<Product>>  GetAllAsync();
       Task<Product> GetOneAsync(Guid id);
        Task CreateAsync(Product product);
        Task Delete(Guid id);
        Task Update(Product product);
    }

    public class ProductService : IProductService
    {
        private readonly AppDataContext _appDataContext;

        public ProductService(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async Task<IEnumerable<Product>>  GetAllAsync()
        {
            var listSubsections = _appDataContext.Products.AsNoTracking().
                Include(product =>product.SubSection).
                ThenInclude(section =>section.Section );
            await listSubsections.ToListAsync();
            return listSubsections;
        }

        public async Task<Product> GetOneAsync(Guid id)
        {
            return await _appDataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            
        }

        public async Task CreateAsync(Product product)
        {   product.PictureGeneral= "C:\\Users\\heroe\\OneDrive\\Изображения\\Снимки экрана\\" + product.PictureGeneral;
            await _appDataContext.Products.AddAsync(product);
          await _appDataContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _appDataContext.Products.Remove(_appDataContext.Products.FirstOrDefault(product => product.Id == id));
           await _appDataContext.SaveChangesAsync();
        }

        public Task Update(Product product)
        {
            _appDataContext.Products.Update(product);
            return _appDataContext.SaveChangesAsync();
        }
    }
}
