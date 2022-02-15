using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        Task SavePicture(Product product);
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

        public async Task<Product> GetOneAsync(Guid id)
        {
            return await _appDataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            
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

        public Task Update(Product product)
        {
            _appDataContext.Products.Update(product);
            return _appDataContext.SaveChangesAsync();
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
                string path1 = Path.Combine(pathString + "/image/", fileName + extension);
                await using (FileStream fs = new FileStream(path1, FileMode.Create))
                {
                    await product.PictureSecondFile.CopyToAsync(fs);
                }
                var pathString2 = _hostEnvironment.WebRootPath;
                string fileName2 = Path.GetFileNameWithoutExtension(product.PictureSubSecondFile.FileName);
                string extension2 = Path.GetExtension(product.PictureSubSecondFile.FileName);
                product.PictureSubSecond = fileName1 + extension1;
                string path2 = Path.Combine(pathString + "/image/", fileName + extension);
                await using (FileStream fs = new FileStream(path2, FileMode.Create))
                {
                    await product.PictureSubSecondFile.CopyToAsync(fs);
                }
        }
    }
}
