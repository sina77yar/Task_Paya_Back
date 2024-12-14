using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Drawing;
using TaskPaya_Back.Application.Common.Utilities.Common;
using TaskPaya_Back.Application.Repositories.Interfaces;
using TaskPaya_Back.Persistence.Repositories.Services;


namespace TaskPaya_Back.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region constructor
        private IProductService productService;
        private IWebHostEnvironment environment;

        public ProductsController(IProductService productService, IWebHostEnvironment environment)
        {
            this.productService = productService;
            this.environment = environment;
        }
        #endregion
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productService.GetAllProducts();
            return JsonResponseStatus.Success(products);
        }

        [HttpGet("GetSingleProduct/{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            var product = await productService.GetProductById(id);
            if (product == null)
            {
                return JsonResponseStatus.Error();
            }
            else
            {
                return JsonResponseStatus.Success(product);
            }
        }
        [HttpGet("EditProductProfit")]
        public async Task<IActionResult> EditProductProfit(int id, int profit)
        {
            await productService.EditProductProfit(id, profit);
            return JsonResponseStatus.Success();
        }





    }
}
