using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.DTOs.Order
{
    public class OrderPurchaseViewModel
    {
        public int Id { get; set; }

        public AddressViewModel AddressVM { get; set; }

        [Display(Name = "کد تخفیف")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [AllowNull]
        public string? NameCode { get; set; }

        [AllowNull]
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
