using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.DTOs.Order
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        [MaxLength(1000)]
        [Required]
        [Display(Name = "آدرس")]
        public string Address { get; set; } = string.Empty;

        public int? CityId { get; set; }

        public CityViewModel? City { get; set; }

        public int? ProvinceId { get; set; }

        public ProvinceViewModel? ProvinceVM { get; set; } 

        [MaxLength(100)]
        [Required]
        [Display(Name = "نام و نام خانوادگی گیرنده")]

        public string CustomerFullName { get; set; } = string.Empty;


        [MaxLength(20)]
        [Required]
        [Display(Name = "تلفن گیرنده")]

        public string CustomerPhoneNum { get; set; } = string.Empty;

        [MaxLength(30)]
        [Required]
        [Display(Name ="کد پستی")]
        public string PostalCode { get; set; } = string.Empty;
    }
}
