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
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(17)]
        public string Name { get; set; }

        [AllowNull]
        [MaxLength(26)]
        public string Slug { get; set; }

        [Required]
        [ForeignKey("Province")]
        public int ProvinceId { get; set; }

        public Province Province { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
