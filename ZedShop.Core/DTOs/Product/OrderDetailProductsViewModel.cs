using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Product
{
    public class OrderDetailProductsViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public double SellPrice { get; set; }

        public string ProductImageName { get; set; } = string.Empty;


    }
}
