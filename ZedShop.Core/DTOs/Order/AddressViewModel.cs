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

        [Required]
        [Display(Name = "آدرس")]
        [MaxLength(1000, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Address { get; set; } = string.Empty;

        public int? CityId { get; set; }

        public CityViewModel? City { get; set; }

        public int? ProvinceId { get; set; }

        public ProvinceViewModel? ProvinceVM { get; set; } 

        [Required]
        [Display(Name = "نام و نام خانوادگی گیرنده")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string CustomerFullName { get; set; } = string.Empty;


        [Required]
        [Display(Name = "تلفن گیرنده")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string CustomerPhoneNum { get; set; } = string.Empty;

        [Required]
        [Display(Name ="کد پستی")]
        [MaxLength(30, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]

        public string PostalCode { get; set; } = string.Empty;
        
        [Required]
        [Display(Name ="پلاک خانه")]
        [MaxLength(30, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string HouseNumber { get; set; } = string.Empty;
    }
}
