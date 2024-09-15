using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class PostDelivery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [AllowNull]
        [MaxLength(500)]
        public string? Description { get; set; }

        [AllowNull]
        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
