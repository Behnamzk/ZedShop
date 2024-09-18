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
    public class OrderindexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "وضعیت")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "تاریخ به‌روزرسانی")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "توضیحات")]
        public string OrderDescription { get; set; } = string.Empty;

    }
}
