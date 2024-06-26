using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;
        private IUserService userService;

        public ProductController(IProductService _productService, IUserService _userService)
        {
            productService = _productService;
            userService = _userService;
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

            var comments = productService.GetAllProductsComment(product.ProductId);

            List<Comment> pComments = comments.ToList();

            CommentViewModel commentViewModel = new CommentViewModel();

            var username = User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                User user = userService.GetUserByUserName(username);

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
                commentViewModel = commentViewModel
            };

            return View(proVM);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddComment(ShowProductViewModel showProductViewModel)
        {
            CommentViewModel commentViewModel = showProductViewModel.commentViewModel;

            if (commentViewModel == null)
            {
                return RedirectToAction("Index");
            }

            productService.AddCommentToProduct(commentViewModel);

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
                User user = userService.GetUserByUserName(username);

                if(user != null) {
                    ProductRate OldRate = productService.GetRateOfUser(user.UserId, showProductViewModel.ProductId);

                    if(OldRate != null)
                    {
                        // update old rate
                        OldRate.Rate = rate;
                        productService.UpdateRateOfUser(OldRate);
                    }
                    else
                    {
                        RateViewModel rateViewModel = new RateViewModel()
                        {
                            UserId = user.UserId,
                            PoroductId = showProductViewModel.ProductId,
                            Rate = rate
                        };
                        productService.AddRateToProduct(rateViewModel);
                        //add new rate
                    }
                }


            }

            return RedirectToAction("ShowProduct", new { id = showProductViewModel.ProductId });
        }
    }
}
