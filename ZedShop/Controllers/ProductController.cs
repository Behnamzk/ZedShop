using Microsoft.AspNetCore.Mvc;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;

namespace ZedShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("ShowProduct/{id}")]
        public IActionResult ShowProduct(int id)
        {
            var product = productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }



            ProductViewModel proVM = new ProductViewModel()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Count = product.Count,
                    DisCount = product.Count,
                    IsActivate = product.IsActivate,
                    ProductId = product.ProductId,
                    ProductImageName = product.ProductImageName,
                    SellPrice = Convert.ToDouble(product.SellPrice),
                    ProductCategories = product.ProductCategories.ToList()
                };


            return View(proVM);
        }
    }
}
