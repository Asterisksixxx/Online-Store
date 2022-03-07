using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;using Online_Store.Data;
using Online_Store.Models;
using Online_Store.ViewModels;

namespace Online_Store.Services
{
    public interface IOrderService
    {
        Task CreateAsync(CreateOrderViewModel createOrderViewModel);
        Task<CreateOrderViewModel> FindUser(string login);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDataContext _db;
        public OrderService(AppDataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(CreateOrderViewModel viewModel)
        {
            Order ord = new Order
            {
                UserId = viewModel.UserId,
                Address ="Г. "+ viewModel.AddressCity+" ул. "+viewModel.AddressStreet+" д. "+viewModel.AddressHome+" кв. "+ viewModel.AddressApartment,
                Date = DateTime.Now.Date,
                TotalCost = viewModel.TotalCost,
                User = await _db.Users.FirstOrDefaultAsync(u =>u.Id==viewModel.UserId),
            };
            await _db.Orders.AddAsync(ord);
            await _db.SaveChangesAsync();
        }

        public async Task<CreateOrderViewModel> FindUser(string login)
        {
            var userId = (await _db.Users.FirstOrDefaultAsync(u => u.Login == login)).Id;
            var basket = await _db.Baskets
                .Include(bas =>bas.ListProducts)
                .ThenInclude(pr =>pr.Product )
                .FirstOrDefaultAsync(b => b.UserId == userId);
            
            return new CreateOrderViewModel()
            {
                UserId =userId,
                TotalCost = basket.ListProducts.Sum(pr => pr.SumCost),
            };
        }
    }
}
