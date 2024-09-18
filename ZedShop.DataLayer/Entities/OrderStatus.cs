using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [AllowNull]
        [MaxLength(100)]
        public string? DisplayName { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
    }
}
