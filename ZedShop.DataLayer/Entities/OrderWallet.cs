using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class OrderWallet
    {
        [Key]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Key]
        [ForeignKey("Wallet")]

        public int WalletId { get; set; }

        public Wallet Wallet { get; set; }

        [Required]
        public double price { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }
    }
}
