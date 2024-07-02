using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class ProductRate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Rate { get; set; } = 0;

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
