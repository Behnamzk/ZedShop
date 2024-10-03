using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.DTOs.Product;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.DTOs.Order
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }

        public List<OrderDetailProductsViewModel> Products { get; set; }

        public AddressViewModel AddressVM { get; set; }

        [Display(Name = "کد تخفیف")]
        [AllowNull]
        public Discount? Discount { get; set; }

        [Display(Name = "وضعیت")]
        [AllowNull]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "تاریخ به‌روزرسانی")]
        public string OrderDate { get; set; }

        [Display(Name = "توضیحات")]
        public string OrderDescription { get; set; } = string.Empty;

    }
}
