using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.DTOs.Product
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

        public bool IsActivate { get; set; }

        public double SellPrice { get; set; }

        public float Rating { get; set; } = 1;

        public string ProductImageName { get; set; } = string.Empty;

        public List<ProductCategory>? ProductCategories { get; set; }
    }
}
