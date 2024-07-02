using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;
using ZedShop.DataLayer.Migrations;

namespace ZedShop.Core.DTOs.Product
{
    public class ShowProductViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Count { get; set; }

        public bool IsActivate { get; set; }

        public double DisCount { get; set; }

        public double SellPrice { get; set; }

        public string ProductImageName { get; set; } = string.Empty;

        public CommentViewModel? CommentViewModel { get; set; }
        public int Rating { get; set; } = 0;
        public List<ProductCategory>? ProductCategories { get; set; }
        public List<DataLayer.Entities.Comment>? ProductComments { get; set; }
    }
}
