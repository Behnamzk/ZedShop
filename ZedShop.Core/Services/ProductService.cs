using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services
{
    public class ProductService : IProductService
    {
        private ZedShopContext context;
        private IUserService userService;

        public ProductService(ZedShopContext _context, IUserService _userService)
        {
            context = _context;
            userService = _userService;
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.ToList();

        }

        public List<Product> GetAllProducts(int count)
        {
            //TODO

            throw new NotImplementedException();
        }

        public bool DecreaseProductCount(int product_id, int count)
        {

            Product product = GetProduct(product_id);

            if (product != null)
            {
                if (product.Count - count > 0)
                {
                    product.Count -= count;
                    context.Products.Update(product);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool IncreaseProductCount(int product_id, int count)
        {
            Product product = GetProduct(product_id);

            if (product != null)
            {
                product.Count += count;
                context.Products.Update(product);
                context.SaveChanges();
                return true;
            }

            return false;
        }
        public Product GetProduct(int product_id)
        {
            return context.Products.Include(c=>c.ProductCategories).ThenInclude(o=>o.Category).SingleOrDefault(c => c.ProductId == product_id);

        }

        public IQueryable<Comment> GetAllProductsComment(int product_id)
        {
            return context.Comments.Include(c=>c.User).Where(c=>c.ProductId == product_id);

		}

        public bool AddCommentToProduct(CommentViewModel commentViewModel)
        {
            if(commentViewModel == null)
            {
                return false;
            }

            Comment comment = new Comment()
            {
                CommentText = commentViewModel.Content,
                ProductId = commentViewModel.PoroductId,
                UserId = commentViewModel.UserId,
                CommentDate=DateTime.Now
               
            };

            context.Comments.Add(comment);
            context.SaveChanges();
            return true;
        }

        #region Rate
        public bool AddRateToProduct(RateViewModel rateViewModel)
        {
            if(rateViewModel  == null)
            {
                return false;
            }

            ProductRate rate = new ProductRate()
            {
                ProductId = rateViewModel.PoroductId,
                UserId = rateViewModel.UserId,
                Rate = rateViewModel.Rate
            };

            context.Rates.Add(rate);
            context.SaveChanges();

            return true;
        }

        public bool UpdateRateOfUser(ProductRate productRate)
        {

            context.Rates.Update(productRate);
            context.SaveChanges();

            return true;
        }

        public float GetAVGRateOfProduct(int prodcut_id)
        {
            return (float)context.Rates.Where(u=>u.ProductId == prodcut_id).Average(r => r.Rate);
        }

        public ProductRate GetRateOfUser(int user_id, int product_id)
        {
           return context.Rates.SingleOrDefault(u => u.UserId == user_id && u.ProductId == product_id);

        }
        #endregion
    }
}
