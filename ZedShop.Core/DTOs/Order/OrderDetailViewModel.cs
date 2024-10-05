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

        public double TotalPrice { get; set; } = 0;
        public double ProductsPrice { get; set; } = 0;

        public List<OrderDetailProductsViewModel> Products { get; set; }

        [Display(Name = "آدرس")]
        [AllowNull]
        public string? AddressVM { get; set; } = string.Empty;

        [Display(Name = "کد تخفیف")]
        [AllowNull]
        public string? Discount { get; set; } = string.Empty;

        [Display(Name = "وضعیت")]
        [AllowNull]
        public string? OrderStatus { get; set; } = string.Empty;

        [Display(Name = "تاریخ به‌روزرسانی")]
        [AllowNull]
        public string? OrderDate { get; set; } = string.Empty;

        [Display(Name = "پست")]
        [AllowNull]
        public string? PostDelivery { get; set; } = string.Empty;

        [Display(Name = "هزینه پست")]
        [AllowNull]
        public double PostPrice { get; set; } =0;

        [Display(Name = "توضیحات")]
        public string OrderDescription { get; set; } = string.Empty;

    }
}
