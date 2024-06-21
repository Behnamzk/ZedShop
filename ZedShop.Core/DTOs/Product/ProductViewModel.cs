using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Product
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public bool IsActivate { get; set; }

        public double DisCount { get; set; }

        public double SellPrice { get; set; }

        public string ProductImageName { get; set; }
    }
}
