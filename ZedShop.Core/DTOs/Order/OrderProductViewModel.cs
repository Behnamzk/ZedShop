using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Order
{
    public class OrderProductViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public int ProdcutCount { get; set; } = 1;
        public double SellPrice { get; set; }       
        public string ProductImageName { get; set; } = string.Empty;
    }
}
