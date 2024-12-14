using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Application.Repositories.Interfaces;
using TaskPaya_Back.Application.Repositories;
using TaskPaya_Back.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace TaskPaya_Back.Persistence.Repositories.Services
{
    public class ProductService : IProductService
    {
        private IGenericRepository<Product> productRepository;
        public ProductService(
            IGenericRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<long> AddProduct(Product product)
        {
            await productRepository.AddEntity(product);
            await productRepository.SaveChanges();
            return product.Id;
        }

        public async Task UpdateProduct(Product product)
        {
            productRepository.UpdateEntity(product);
            await productRepository.SaveChanges();

        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await productRepository.GetEntitiesQuery().ToListAsync();
        }
        public void Dispose()
        {
            productRepository?.Dispose();
        }


        public async Task<Product> GetProductById(long productId)
        {
            var a = await productRepository.GetEntitiesQuery().FirstOrDefaultAsync(a => a.Id == productId);
            return a;
        }

        public async Task EditProductProfit(int id, int profit)
        {
            var product = await productRepository.GetEntitiesQuery().FirstOrDefaultAsync(a => a.Id == id);
            product.Profit = profit;
            productRepository.UpdateEntity(product);
            await productRepository.SaveChanges();
        }
    }
}
