using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Json;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ProductController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AllProductsUserSearch(string userInput = "")
        {

            var products = _productService.GetAllProductsBySearchName(userInput);

            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            foreach (var p in products)
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


            }

            return PartialView("_BoxProductsContainer", productViewModels);
        }

        [HttpGet]
        public IActionResult AllProductsPartial(int? categoryId)
        {

            List<Product> products = new List<Product>();   
            if(categoryId == -1)
            {
                products = _productService.GetAllProducts();
            }
            else
            {
                products = _productService.GetAllProductsOfCategory(categoryId ?? 0);
            }

            
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            foreach (var p in products)
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

            }

            return PartialView("_BoxProductsContainer", productViewModels);
        }

        [Route("AllProduct")]
        public IActionResult AllProducts()
        {
            // select top 6 products
            var products = _productService.GetAllProducts(6);
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            foreach (var p in products)
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
            }

            ViewBag.Categories = _productService.GetAllCategory();

            return View(productViewModels);
        }

        [Route("ShowProduct/{id}")]
        public IActionResult ShowProduct(int id)
        {
            var product = _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            var comments = _productService.GetAllProductsComment(product.ProductId);

            List<Comment> pComments = comments.ToList();

            CommentViewModel commentViewModel = new CommentViewModel();

            var username = User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                User user = _userService.GetUserByUserName(username);

                commentViewModel.PoroductId = product.ProductId;
                commentViewModel.UserId = user.UserId;

            }

            ShowProductViewModel proVM = new ShowProductViewModel()
            {
                Name = product.Name,
                Description = product.Description,
                Count = product.Count,
                DisCount = product.Count,
                IsActivate = product.IsActivate,
                ProductId = product.ProductId,
                ProductImageName = product.ProductImageName,
                SellPrice = Convert.ToDouble(product.SellPrice),
                ProductCategories = product.ProductCategories.ToList(),
                ProductComments = pComments,
                CommentViewModel = commentViewModel
            };

            return View(proVM);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddComment(ShowProductViewModel showProductViewModel)
        {
            CommentViewModel commentViewModel = showProductViewModel.CommentViewModel;

            if (commentViewModel == null)
            {
                return RedirectToAction("Index");
            }

            _productService.AddCommentToProduct(commentViewModel);

            return RedirectToAction("ShowProduct", new { id = commentViewModel.PoroductId });
        }


        [HttpPost]
        [Authorize]
        public IActionResult AddRating(ShowProductViewModel showProductViewModel)
        {
            if (showProductViewModel.Rating == 0)
            {
                return RedirectToAction("ShowProduct", new { id = showProductViewModel.ProductId });
            }

            int rate = showProductViewModel.Rating;

            var username = User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                User user = _userService.GetUserByUserName(username);

                if (user != null)
                {
                    ProductRate OldRate = _productService.GetRateOfUser(user.UserId, showProductViewModel.ProductId);

                    if (OldRate != null)
                    {
                        // update old rate
                        OldRate.Rate = rate;
                        _productService.UpdateRateOfUser(OldRate);
                    }
                    else
                    {
                        //add new rate
                        RateViewModel rateViewModel = new RateViewModel()
                        {
                            UserId = user.UserId,
                            PoroductId = showProductViewModel.ProductId,
                            Rate = rate
                        };
                        _productService.AddRateToProduct(rateViewModel);

                    }
                }


            }

            return RedirectToAction("ShowProduct", new { id = showProductViewModel.ProductId });
        }
    }
}
