using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;
using Online_Store.ViewModels;

namespace Online_Store.Services
{
    public interface IProductService
    {
      Task<IEnumerable<Product>>  GetAllAsync();
      Task<IEnumerable<Models.Product>> GetAllFromSubSectionsAsync(Guid id);
       Task<Product> GetOneAsync(Guid id);
        Task CreateAsync(Product product);
        Task Delete(Guid id);
        Task<UpdateProductViewModel> FindProductForUpdate(Guid id);
        Task Update(UpdateProductViewModel viewModel);
        Task SavePicture(Product product);
        Task DecCount(Guid userId);
    }

    public class ProductService : IProductService
    {
        private readonly AppDataContext _appDataContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductService(AppDataContext appDataContext, IWebHostEnvironment hostEnvironment)
        {
            _appDataContext = appDataContext;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IEnumerable<Product>>  GetAllAsync()
        {
            var listProduct = _appDataContext.Products
                .Include(p => p.SubSection).ThenInclude(s => s.Section);
            return await listProduct.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllFromSubSectionsAsync(Guid id)
        {
            return await _appDataContext.Products.Where(p => p.SubSectionId == id).ToListAsync();
        }

        public async Task<Product> GetOneAsync(Guid id)
        {
            return await _appDataContext.Products.Include(p=>p.SubSection)
                .FirstOrDefaultAsync(p => p.Id == id);
            
        }

        public async Task CreateAsync(Product product)
        {   

            await _appDataContext.Products.AddAsync(product);
          await _appDataContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _appDataContext.Products.Remove(_appDataContext.Products.FirstOrDefault(product => product.Id == id)!);
           await _appDataContext.SaveChangesAsync();
        }

        public async Task<UpdateProductViewModel> FindProductForUpdate(Guid id)
        {
            var product = await _appDataContext.Products
                .AsNoTracking()
                .Include(p => p.SubSection)
                .FirstOrDefaultAsync(product => product.Id == id);
            return new UpdateProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Cost =Convert.ToString(product.Cost),
                Count = product.Count,
                Information = product.Information,
                ViewCount = product.ViewCount,
                SubSectionId = product.SubSectionId,
                Score = product.Score,
                OrderCount = product.OrderCount,
                SubSection = product.SubSection
            };
        }

        public async Task Update(UpdateProductViewModel viewModel)
        {
            var oldproduct = await _appDataContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(pr => pr.Id == viewModel.Id);
            viewModel.Cost = viewModel.Cost.Replace(".", ",");
            var cost = Convert.ToDecimal(viewModel.Cost);
            Product product = new Product()
            {
                Id = viewModel.Id,
                Count = viewModel.Count,
                Cost = cost,
                Information = viewModel.Information,
                Name = viewModel.Name,
                OrderCount = viewModel.OrderCount,
                Score = viewModel.Score,
                ViewCount = viewModel.ViewCount,
                SubSectionId = viewModel.SubSectionId,
                PictureGeneral = oldproduct.PictureGeneral,
                PictureSecond = oldproduct.PictureSecond,
                PictureSubSecond = oldproduct.PictureSubSecond,
                PictureGeneralFile = oldproduct.PictureGeneralFile,
                PictureSecondFile = oldproduct.PictureSecondFile,
                PictureSubSecondFile = oldproduct.PictureSubSecondFile,
                Reviews = oldproduct.Reviews,
            };
            _appDataContext.Products.Update(product);
            await _appDataContext.SaveChangesAsync();
        }

        public async Task SavePicture(Product product)
        {
            
                var pathString = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(product.PictureGeneralFile.FileName);
                string extension = Path.GetExtension(product.PictureGeneralFile.FileName);
                product.PictureGeneral = fileName + extension;
                string path = Path.Combine(pathString + "/image/", fileName + extension);
                await using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    await product.PictureGeneralFile.CopyToAsync(fs);
                }

                var pathString1 = _hostEnvironment.WebRootPath;
                string fileName1 = Path.GetFileNameWithoutExtension(product.PictureSecondFile.FileName);
                string extension1 = Path.GetExtension(product.PictureSecondFile.FileName);
                product.PictureSecond = fileName1 + extension1;
                string path1 = Path.Combine(pathString1 + "/image/", fileName1 + extension1);
                await using (FileStream fs = new FileStream(path1, FileMode.Create))
                {
                    await product.PictureSecondFile.CopyToAsync(fs);
                }
                var pathString2 = _hostEnvironment.WebRootPath;
                string fileName2 = Path.GetFileNameWithoutExtension(product.PictureSubSecondFile.FileName);
                string extension2 = Path.GetExtension(product.PictureSubSecondFile.FileName);
                product.PictureSubSecond = fileName2 + extension2;
                string path2 = Path.Combine(pathString2 + "/image/", fileName2 + extension2);
                await using (FileStream fs = new FileStream(path2, FileMode.Create))
                {
                    await product.PictureSubSecondFile.CopyToAsync(fs);
                }
        }

        public async Task DecCount(Guid userId)
        {

           var basket= await _appDataContext.Baskets
               .AsNoTracking()
                .Include(b => b.ListProducts)
                .ThenInclude(p =>p.Product )
                .FirstOrDefaultAsync(b => b.UserId == userId);
           foreach (var baskProd in basket.ListProducts)
           {
              var product= await _appDataContext.Products.FirstOrDefaultAsync(p => p.Id == baskProd.Product.Id);
              product.Count-=Convert.ToInt32(baskProd.Count);
               _appDataContext.Products.Update(product);
               await _appDataContext.SaveChangesAsync();
              product.OrderCount++;
           }
           basket.ListProducts.Clear();
           await _appDataContext.SaveChangesAsync();
        }
    }
}
