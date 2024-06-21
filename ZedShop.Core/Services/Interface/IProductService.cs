using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services.Interface
{
    public interface IProductService
    {
        Product GetProduct(int product_id);

        List<Product> GetAllProducts();
        List<Product> GetAllProducts(int count);

        public bool DecreaseProductCount(int product_id, int count);
        public bool IncreaseProductCount(int product_id, int count);


    }
}
