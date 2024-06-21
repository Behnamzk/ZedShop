using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Type { get; set; }

        [MaxLength(500)]
        public string Detail { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Value { get; set; }

        public Wallet Wallet { get; set; }

        [ForeignKey("Wallet")]
        public int WalletId { get; set; }
    }
}
