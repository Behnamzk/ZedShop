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
        [Display(Name = "هزینه")]

        public double TotalPrice { get; set; }

        [AllowNull]
        [MaxLength(40)]
        [Display(Name = "کد پیگیری")]

        public string? TrackingCode { get; set; }

        [AllowNull]
        [MaxLength(500)]
        [Display(Name = "توضیحات")]

        public string? Description { get; set; }

        [AllowNull]
        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
