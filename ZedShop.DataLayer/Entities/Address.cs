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
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(1000)]
        [Required]
        public string AddressContent { get; set; } = string.Empty;

        [MaxLength(30)]
        [Required]
        public string PostalCode { get; set; } = string.Empty;

        [MaxLength(20)]
        [Required]
        public string HouseNumber { get; set; } = string.Empty;

        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        [ForeignKey("Province")]
        public int ProvinceId { get; set; }

        public Province Province { get; set; }


        [AllowNull]
        [ForeignKey("User")]
        public int? UserId { get; set; }

        public User? User { get; set; }

        [MaxLength(100)]
        [Required]
        public string CustomerFullName { get; set; } = string.Empty;


        [MaxLength(20)]
        [Required]
        public string CustomerPhoneNum { get; set; } = string.Empty;
    }
}
