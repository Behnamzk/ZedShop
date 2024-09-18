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
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [AllowNull]
        public DateTime FinalDate { get; set; }

        [AllowNull]
        [MaxLength(300)]
        public string? Description { get; set; }

        [AllowNull]
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        [AllowNull]
        [ForeignKey("PostDelivery")]
        public int? PostDeliveryId { get; set; }
        public PostDelivery? PostDelivery { get; set; }

        [AllowNull]
        [ForeignKey("Discount")]
        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }

        [ForeignKey("OrderStatus")]
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
        public ICollection<OrderWallet> OrderWallets { get; set; }


    }
}
