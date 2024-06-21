using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services
{
    public class ProductService : IProductService
    {
        private ZedShopContext context;

        public ProductService(ZedShopContext _context)
        {
            context = _context;
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
            return context.Products.SingleOrDefault(c => c.ProductId == product_id);

        }
    }
}
