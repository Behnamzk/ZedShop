using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.DTOs.Order
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [MaxLength(1000, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Address { get; set; } = string.Empty;

        [AllowNull]
        public int? CityId { get; set; }

        public CityViewModel? City { get; set; }

        [AllowNull]
        public int? ProvinceId { get; set; }

        public ProvinceViewModel? ProvinceVM { get; set; } 

        [Display(Name = "نام و نام خانوادگی گیرنده")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string CustomerFullName { get; set; } = string.Empty;


        [Display(Name = "تلفن گیرنده")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string CustomerPhoneNum { get; set; } = string.Empty;

        [Display(Name ="کد پستی")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [MaxLength(30, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]

        public string PostalCode { get; set; } = string.Empty;
        
        [Display(Name ="پلاک خانه")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [MaxLength(30, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string HouseNumber { get; set; } = string.Empty;
    }
}
