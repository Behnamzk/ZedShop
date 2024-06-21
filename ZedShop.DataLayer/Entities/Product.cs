using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public bool IsActivate { get; set; }

        public double DisCount { get; set; }

        [Required]
        public bool IsShow { get; set; }

        [Required]
        public DateTime BuyDate { get; set; }

        [Required]
        public double SellPrice { get; set; }

        [MaxLength(50)]
        public string ProductImageName { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductOldPrice> ProductOldPrices { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }


    }
}
