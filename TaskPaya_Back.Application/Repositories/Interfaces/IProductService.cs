using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Domain.Entities.Products;

namespace TaskPaya_Back.Application.Repositories.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task<long> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(long productId);
        Task EditProductProfit(int id, int profit);


    }
}
