
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class Province
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(19)]
        public string Name { get; set; }

        [Required]
        [MaxLength(17)]
        public string Slug { get; set; }

        [Required]
        [MaxLength(3)]
        public string Tel_Prefix { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
