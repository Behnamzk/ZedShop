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

        [MaxLength(1000)]
        [AllowNull]
        public string Address { get; set; }

        [MaxLength(300)]
        [AllowNull]
        public string City { get; set; }

        [MaxLength(300)]
        [AllowNull]
        public string Province { get; set; }

        [Required]
        public bool Status { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }


    }
}
