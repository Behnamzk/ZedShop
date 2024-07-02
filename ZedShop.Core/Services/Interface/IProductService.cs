using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.DTOs.Product;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services.Interface
{
    public interface IProductService
    {
        Product GetProduct(int productId);
        List<Product> GetAllProducts();
        List<Product> GetAllProducts(int count);
		IQueryable<Comment> GetAllProductsComment(int productId);
        bool AddRateToProduct(RateViewModel rateViewModel);
        bool UpdateRateOfUser(ProductRate productRate);
        float GetAVGRateOfProduct(int prodcutId);
        ProductRate GetRateOfUser(int userId, int prodcutId);
        bool AddCommentToProduct(CommentViewModel commentViewModel);
        bool DecreaseProductCount(int prodcutId, int count);
        bool IncreaseProductCount(int prodcutId, int count);

    }
}
