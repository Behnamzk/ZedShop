using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZedShop.Core.DTOs.Home;
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
        private readonly IHomeService _homeService;

        public HomeController(IProductService productService, IHomeService homeService)
        {
            _productService = productService;
            _homeService = homeService;
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
                float rating = _productService.GetAVGRateOfProduct(p.ProductId);

                productViewModels.Add(new ProductViewModel()
                {
                    Name = p.Name,
                    Description = p.Description,
                    IsActivate = p.IsActivate,
                    ProductId = p.ProductId,
                    ProductImageName = p.ProductImageName,
                    SellPrice = Convert.ToDouble(p.SellPrice),
                    Rating = rating
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


        [Route("/AboutUs")]
        public IActionResult ABoutUs()
        {
            return View();
        }


        [Route("/SiteGuide")]
        public IActionResult SiteGuide()
        {
            return View();
        }


        [Route("/EarningIncome")]
        public IActionResult EarningIncome()
        {
            return View();
        }


        [Route("/Informations")]
        public IActionResult Informations()
        {
            return View();
        }

        [Route("/SiteRoles")]
        public IActionResult SiteRoles()
        {
            return View();
        }


        [Route("/WorkWithUs")]
        public IActionResult WorkWithUs()
        {
            return View();
        }


        [Route("/Opinions")]
        public IActionResult Opinions()
        {
            int numberOfOpinions = 10;
            var opinions = _homeService.GetOpinions(numberOfOpinions);

            List<OpinionViewModel> opinionViewModels = new List<OpinionViewModel>();

            foreach(var opinion in opinions)
            {
                
                opinionViewModels.Add(new OpinionViewModel()
                {
                    Content = opinion.OpinionText,
                    Date = opinion.OpinionDate,
                    UserName = opinion.User.UserName,
                    UserAvatar = opinion.User.UserAvatar

                });
            }

            ViewBag.Opinions = opinionViewModels;

            return View();
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
