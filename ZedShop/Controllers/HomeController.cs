using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserService _userService;

        public HomeController(IProductService productService, IHomeService homeService, IUserService userService)
        {
            _productService = productService;
            _homeService = homeService;
            _userService = userService;
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

            var opinions = _homeService.GetOpinions(5);

            List<OpinionViewModel> opinionViewModels = new List<OpinionViewModel>();

            foreach(var opinion in opinions)
            {
                opinionViewModels.Add(new OpinionViewModel()
                {
                    Content = opinion.OpinionText,
                    UserAvatar = opinion.User.UserAvatar,
                    UserName = opinion.User.UserName,
                    Date = opinion.OpinionDate
                });
            }

            ViewBag.Opinions = opinionViewModels;

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

            OpinionViewModel opinionView = new OpinionViewModel();

            var username = User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                User user = _userService.GetUserByUserName(username);

                opinionView.UserId = user.UserId;
            }
            else
            {
                // set invalid userId 
                opinionView.UserId = -1;
            }

            

            return View(opinionView);
        }


        [HttpPost]
        [Authorize]
        public IActionResult AddOpinion(OpinionViewModel opinionView)
        {

            if (opinionView.UserId == -1)
            {
                return RedirectToAction("Index");
            }

            _homeService.AddOpinion(opinionView);

            return RedirectToAction("Opinions");
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
