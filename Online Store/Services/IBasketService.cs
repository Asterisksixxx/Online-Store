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
    public interface IBasketService
    {
        Task CreateAsync(Guid userId);
        Task UpdateAsync(Guid productId,string login, uint count);
        Task<IndexBasket> GetOneAsync(string login);
        Task RemoveBasketProduct(Guid productId, uint count,Guid basketId);
    }
    public class BasketService : IBasketService
    {
        private readonly AppDataContext _appDataContext;
        private readonly IBasketProductService _basketProductService;

        public BasketService(AppDataContext appDataContext, IBasketProductService basketProductService)
        {
            _appDataContext = appDataContext;
            _basketProductService = basketProductService;
        }

        public async Task CreateAsync(Guid userId)
        {
            Basket basket = new Basket
            {
                ListProducts = new List<BasketProduct>(),
                User =await _appDataContext.Users.FirstOrDefaultAsync(user =>user.Id==userId),
                UserId = userId
            };
            await _appDataContext.Baskets.AddAsync(basket);
            await _appDataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid productId, string login, uint count)
        {
            var user = await _appDataContext.Users.FirstOrDefaultAsync(u => u.Login == login);
            var basket = await _appDataContext.Baskets
              .AsNoTracking()
              .Include(b=>b.ListProducts)
              .ThenInclude(b=>b.Product)
              .FirstOrDefaultAsync(b => b.UserId == user.Id);
            var productInBasket = basket.ListProducts.FirstOrDefault(p => p.Product.Id == productId);
            var productOld = productInBasket;

            if (basket.ListProducts.Contains(productInBasket))
            {
                basket.ListProducts.Remove(productInBasket);
                productInBasket.Count = productInBasket.Count + count;
                productInBasket.SumCost = productInBasket.Count
                    * productInBasket.Product.Cost;
                basket.ListProducts.Add(productInBasket);
                _appDataContext.BasketProducts.Remove(productOld);
                await _appDataContext.SaveChangesAsync();
            }
            if (productInBasket != null)
            {
                //BasketProduct basketProduct = new BasketProduct()
                //{
                //    Product = await _appDataContext.Products.FirstOrDefaultAsync(p => p.Id == productId),
                //    Count = basketItem.Count,
                //    BasketId = basket.Id,
                //    Basket = basket,
                //};
                //await _basketProductService.CreateAsync(basketProduct);
            }
            else
            {
                 productInBasket = new BasketProduct()
                {
                    Product = await _appDataContext.Products
                    .FirstOrDefaultAsync(p => p.Id == productId),
                    Count = count,
                    BasketId = basket.Id,
                    SumCost = count * (await _appDataContext.Products
                    .FirstOrDefaultAsync(p => p.Id == productId)).Cost
                };
            }
            await _basketProductService.CreateAsync(productInBasket);
            await _appDataContext.SaveChangesAsync();
        }

        public async Task RemoveBasketProduct(Guid productId, uint count, Guid basketId)
        {
            var basketProduct = await _appDataContext
                .BasketProducts
                .Include(p=>p.Product)
                .FirstOrDefaultAsync(p => p.Id == productId);
            var basket = await _appDataContext.Baskets.FirstOrDefaultAsync(b => b.Id == basketId);
            if (basketProduct.Count > count)
            {
                basketProduct.Count -= count;
                basketProduct.SumCost = basketProduct.Count * basketProduct.Product.Cost;
                _appDataContext.BasketProducts.Update(basketProduct);
               //basket.ListProducts.Remove(basket.ListProducts.FirstOrDefault(p => p.Id == basketProduct.Id));
               // basket.ListProducts.Add(basketProduct);
                await _appDataContext.SaveChangesAsync();
            }
            else
            {
                _appDataContext.BasketProducts.Remove(basketProduct);
                await _appDataContext.SaveChangesAsync();
            }
        }

        public async Task<IndexBasket> GetOneAsync(string login)
        {
            var user =await _appDataContext.Users.FirstOrDefaultAsync(u =>u.Login== login);
           var bask= await _appDataContext.Baskets.FirstOrDefaultAsync(basket => basket.UserId == user.Id);
           var basketProduct =await _basketProductService.GetOneForUser(bask.Id);
           return new IndexBasket
           {
               basketId=bask.Id,
               UserId = bask.UserId,
               ListProducts = basketProduct.ToList(),
               User = bask.User
           };
        }
    }
}
