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

        public ProductService(ZedShopContext context)
        {
            _context = context;
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
                if (product.Count - count >= 0)
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

        public bool IsProductExist(int productId)
        {
            return _context.Products.Any(p => p.ProductId == productId);
        }

        public bool UpdateProduct(Product product)
        {
            if (product == null)
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
                    if (product.ProductImageName != "noimage_product.png")
                    {
                        string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/Image", product.ProductImageName);
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

        public bool AddProduct(Product product, IFormFile imgProduct)
        {
            if (product == null)
            {
                return false;
            }
            else
            {
                if (imgProduct != null && imgProduct.IsImage())
                {
                    if (product.ProductImageName != "noimage_product.png")
                    {
                        string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products/Image", product.ProductImageName);
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
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
        }
        #endregion


        public int GetAllCommentsCount(int filterId)
        {
            switch (filterId)
            {
                case -1:
                    // All products except Deleted products
                    return _context.Comments.Count();

                case 0:
                    // IsShow == false
                    return _context.Comments.Where(u => u.IsShow == false).Count();

                default:
                    return _context.Comments.Count();

            }
        }

        public List<Comment> GetAllCommentsPaged(int page, int pageSize, int filterId)
        {
            switch (filterId)
            {
                case -1:
                    // All products except Deleted products
                    return _context.Comments.Include(c=>c.User).Include(c => c.Product).ToPaged(page, pageSize).ToList();

                case 0:
                    // IsShow == false
                    return _context.Comments.Include(c => c.User).Include(c => c.Product).Where(u => u.IsShow == false).ToPaged(page, pageSize).ToList();

                default:
                    return _context.Comments.Include(c => c.User).Include(c => c.Product).ToPaged(page, pageSize).ToList();

            }
        }

        public bool DeleteComment(int commentId)
        {

            var comment = GetComment(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ShowComment(int commentId)
        {
            var comment = GetComment(commentId);

            comment.IsShow = !comment.IsShow;
            _context.Comments.Update(comment);
            _context.SaveChanges();
            return comment.IsShow;
        }

        public bool IsCommentExist(int commentId)
        {
            return _context.Comments.Any(p => p.Id == commentId);
        }

        public Comment GetComment(int commentId)
        {
            return _context.Comments.Include(c => c.User).Include(c => c.Product).SingleOrDefault(c => c.Id == commentId);

        }

        public int GetAllCategoriesCount(int filterId)
        {
            switch (filterId)
            {
                case -1:
                    return _context.Categories.Count();

                case 0:
                    // IsShow == false
                    return _context.Categories.Where(c => c.IsRoot == true).Count();

                default:
                    return _context.Categories.Count();

            }
        }

        public List<Category> GetAllCategoriesPaged(int page, int pageSize, int filterId)
        {
            switch (filterId)
            {
                case -1:
                    return _context.Categories.Include(c => c.Parent).ToPaged(page, pageSize).ToList();

                case 0:
                    // IsShow == false
                    return _context.Categories.Include(c => c.Parent).Where(c => c.IsRoot == true).ToPaged(page, pageSize).ToList();

                default:
                    return _context.Categories.Include(c => c.Parent).ToPaged(page, pageSize).ToList();

            }
        }

        public bool DeleteCategory(int categoryId)
        {
            var category = GetCategory(categoryId);
            if (category != null)
            {
                if(!_context.ProductCategories.Any(c =>c.CategoryId == categoryId))
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public bool IsCategoryExist(int categoryId)
        {
            return _context.Categories.Any(p => p.Id == categoryId);
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories.Include(c=>c.Parent).SingleOrDefault(c => c.Id == categoryId);

        }

        public bool AddCategory(Category category)
        {
            if (category != null)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpdateCategory(Category category)
        {
            if (category == null)
            {
                return false;
            }
            else
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return true;
            }
        }

        public bool IsCategoryNameExist(string categoryName, int categoryId)
        {
            return _context.Categories.Any(c=>c.Name == categoryName && c.Id != categoryId);
        }
    }
}
