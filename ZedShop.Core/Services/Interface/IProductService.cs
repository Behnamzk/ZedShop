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
        Product GetProduct(int product_id);
        List<Product> GetAllProducts();
        List<Product> GetAllProducts(int count);

		IQueryable<Comment> GetAllProductsComment(int product_id);

        bool AddRateToProduct(RateViewModel rateViewModel);
        bool UpdateRateOfUser(ProductRate productRate);
        float GetAVGRateOfProduct(int prodcut_id);
        ProductRate GetRateOfUser(int user_id, int product_id);
        

        bool AddCommentToProduct(CommentViewModel commentViewModel);
        bool DecreaseProductCount(int product_id, int count);
        bool IncreaseProductCount(int product_id, int count);


    }
}
