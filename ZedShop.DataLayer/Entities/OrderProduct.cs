using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class OrderProduct
    {
        [Key]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Key]
        [ForeignKey("Order")]
        public int OrdrId { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Count { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
