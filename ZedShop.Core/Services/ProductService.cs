using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.Convertors;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Generator;
using ZedShop.Core.Security;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ZedShopContext _context;
        private readonly IUserService _userService;

        public ProductService(ZedShopContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Include(p => p.ProductRates).ToList();

        }

        public List<Product> GetAllProductsOfCategory(int categoryId)
        {
            return _context.Products.Include(p => p.ProductCategories).Include(p => p.ProductRates).Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId)).ToList();

        }

        public List<Product> GetAllProducts(int count)
        {
            return _context.Products.Include(p => p.ProductRates).Take(count).ToList();
        }

        public List<Product> GetAllProductsBySearchName(string userInput)
        {
            return _context.Products.Include(p => p.ProductRates).Where(p => p.Name.Contains(userInput)).ToList();
        }


        public bool DecreaseProductCount(int productId, int count)
        {

            Product product = GetProduct(productId);

            if (product != null)
            {
                if (product.Count - count > 0)
                {
                    product.Count -= count;
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool IncreaseProductCount(int prodcutId, int count)
        {
            Product product = GetProduct(prodcutId);

            if (product != null)
            {
                product.Count += count;
                _context.Products.Update(product);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
        public Product GetProduct(int productId)
        {
            return _context.Products.Include(c => c.ProductCategories).ThenInclude(o => o.Category).SingleOrDefault(c => c.ProductId == productId);

        }

        public IQueryable<Comment> GetAllProductsComment(int productId)
        {
            return _context.Comments.Include(c => c.User).Where(c => c.ProductId == productId);

        }

        public bool AddCommentToProduct(CommentViewModel commentViewModel)
        {
            if (commentViewModel == null)
            {
                return false;
            }

            Comment comment = new Comment()
            {
                CommentText = commentViewModel.Content,
                ProductId = commentViewModel.PoroductId,
                UserId = commentViewModel.UserId,
                CommentDate = DateTime.Now

            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
            return true;
        }

        #region Rate
        public bool AddRateToProduct(RateViewModel rateViewModel)
        {
            if (rateViewModel == null)
            {
                return false;
            }

            ProductRate rate = new ProductRate()
            {
                ProductId = rateViewModel.PoroductId,
                UserId = rateViewModel.UserId,
                Rate = rateViewModel.Rate
            };

            _context.Rates.Add(rate);
            _context.SaveChanges();

            return true;
        }

        public bool UpdateRateOfUser(ProductRate productRate)
        {

            _context.Rates.Update(productRate);
            _context.SaveChanges();

            return true;
        }

        public float GetAVGRateOfProduct(int productId)
        {
            var rates = _context.Rates.Where(u => u.ProductId == productId);
            if (rates.Any())
            {
                return (float)rates.Average(r => r.Rate);
            }
            else
            {
                return 1.0F;
            }
        }

        public ProductRate GetRateOfUser(int userId, int productId)
        {
            return _context.Rates.SingleOrDefault(u => u.UserId == userId && u.ProductId == productId);

        }

        public List<Category> GetAllCategory()
        {
            return _context.Categories.ToList();
        }

        public int GetAllProductsCount(int filterId)
        {

            switch (filterId)
            {
                case -1:
                    // All products except Deleted products
                    return _context.Products.Where(u => u.IsDelete == false).Count();

                case 0:
                    // Deleted products 
                    return _context.Products.Where(u => u.IsDelete == true).Count();

                case 1:
                    // Count == 0
                    return _context.Products.Where(u => u.IsDelete == false && u.Count == 0).Count();

                case 2:
                    // IsActivate == false
                    return _context.Products.Where(u => u.IsDelete == false && u.IsActivate == false).Count();

                case 3:
                    // IsShow == false
                    return _context.Products.Where(u => u.IsDelete == false && u.IsShow == false).Count();

                default:
                    return _context.Products.Where(u => u.IsDelete == false).Count();

            }
        }

        public List<Product> GetAllProductsPaged(int page, int pageSize, int filterId)
        {
            switch (filterId)
            {
                case -1:
                    // All products except Deleted products
                    return _context.Products.Where(u => u.IsDelete == false).ToPaged(page, pageSize).ToList();

                case 0:
                    // Deleted products 
                    return _context.Products.Where(u => u.IsDelete == true).ToPaged(page, pageSize).ToList();

                case 1:
                    // Count == 0
                    return _context.Products.Where(u => u.IsDelete == false && u.Count == 0).ToPaged(page, pageSize).ToList();

                case 2:
                    // IsActivate == false
                    return _context.Products.Where(u => u.IsDelete == false && u.IsActivate == false).ToPaged(page, pageSize).ToList();

                case 3:
                    // IsShow == false
                    return _context.Products.Where(u => u.IsDelete == false && u.IsShow == false).ToPaged(page, pageSize).ToList();

                default:
                    return _context.Products.Where(u => u.IsDelete == false).ToPaged(page, pageSize).ToList();

            }
        }

        public bool DeleteProduct(int productId)
        {
            if (_context.OrderProducts.Any(u => u.ProductId == productId))
            {
                return false;
            }
            else
            {
                var product = GetProduct(productId);
                if (product != null)
                {
                    product.IsDelete = !product.IsDelete;
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public bool CompleteDeleteProduct(int productId)
        {
            if (_context.OrderProducts.Any(u => u.ProductId == productId))
            {
                return false;
            }
            else
            {
                var product = GetProduct(productId);
                if (product != null)
                {
                    product.IsDelete = true;
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public bool ShowProduct(int productId)
        {
            var product = GetProduct(productId);

            product.IsShow = !product.IsShow;
            _context.Products.Update(product);
            _context.SaveChanges();
            return product.IsShow;

        }

        public bool IsProducExist(int productId)
        {
            return _context.Products.Any(p => p.ProductId == productId);
        }

        public bool UpdateProduct(Product product)
        {
            if(product == null)
            {
                return false;
            }
            else
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return true;
            }
        }

        public bool UpdateProduct(Product product, IFormFile imgProduct)
        {
            if (product == null)
            {
                return false;
            }
            else
            {
                if (imgProduct != null && imgProduct.IsImage())
                {
                    if (product.ProductImageName != "Defult.jpg")
                    {
                        string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(),  "wwwroot/Products/Image", product.ProductImageName);
                        if (File.Exists(deleteimagePath))
                        {
                            File.Delete(deleteimagePath);
                        }
                    }
                    product.ProductImageName = NameGenerator.GenerateUniqueCode() + Path.GetExtension(imgProduct.FileName);

                    ImageConvertor imgResizer = new ImageConvertor();
                    string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/Image", product.ProductImageName);

                    imgResizer.ResizeImage(imgProduct, thumbPath, 750, 500);
                }
                _context.Products.Update(product);
                _context.SaveChanges();
                return true;
            }
        }

        #endregion
    }
}
