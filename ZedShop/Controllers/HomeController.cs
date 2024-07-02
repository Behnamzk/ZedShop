using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Entities;
using ZedShop.Models;

namespace ZedShop.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            int NumberOfProducts = 6;
            int temp_count = 0;

            //TODO get only NumberOfProducts from service
            var products = _productService.GetAllProducts();
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            foreach(var p in products)
            {
                productViewModels.Add(new ProductViewModel()
                {
                    Name = p.Name,
                    Description = p.Description,
                    Count = p.Count,
                    DisCount = p.Count,
                    IsActivate = p.IsActivate,
                    ProductId = p.ProductId,
                    ProductImageName = p.ProductImageName,
                    SellPrice = Convert.ToDouble(p.SellPrice)   
                });

                // remove this part if fix the service
                temp_count += 1;
                if(temp_count >= NumberOfProducts)
                {
                    break;
                }
            }

            return View(productViewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
