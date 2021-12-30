using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Online_Store.Data;
using Online_Store.Models;

namespace Online_Store.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAsync();
        Product GetAsync(Guid id);
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

        public IEnumerable<Product> GetAsync()
        {
            return _appDataContext.Products;
        }

        public  Product GetAsync(Guid id)
        {
            return  _appDataContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public Task CreateAsync(Product product)
        {
            _appDataContext.Products.AddAsync(product);
          return _appDataContext.SaveChangesAsync();
        }

        public void Delete(Guid id)
        {
            _appDataContext.Products.Remove(_appDataContext.Products.FirstOrDefault(product => product.Id == id));
        }

        public Task Update(Product product)
        {
            _appDataContext.Products.Update(product);
            return _appDataContext.SaveChangesAsync();
        }
    }
}
