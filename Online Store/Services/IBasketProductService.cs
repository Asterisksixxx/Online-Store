using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;

namespace Online_Store.Services
{
    public interface IBasketProductService
    {
        Task<IEnumerable<BasketProduct>> GetOneForUser(Guid basketId);
        Task CreateAsync(BasketProduct basketProduct);
    }

    public class BasketProductService : IBasketProductService
    {
        private readonly AppDataContext _appDataContext;

        public BasketProductService(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async Task<IEnumerable<BasketProduct>> GetOneForUser(Guid basketId)
        {
            try
            {
                return await _appDataContext.BasketProducts
                    .Where(b => b.BasketId == basketId)
                    .Include(p =>p.Product)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                return new List<BasketProduct>();
            } 
        }

        public async Task CreateAsync(BasketProduct basketProduct)
        {
            basketProduct.SumCost = basketProduct.Count * basketProduct.Product.Cost;
            await _appDataContext.BasketProducts.AddAsync(basketProduct);
            await _appDataContext.SaveChangesAsync();
        }
    }
}
