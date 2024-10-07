using System.ComponentModel.DataAnnotations;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Web.Areas.Admin.Models.OrdersViewModel
{
    public class OrderViewModelAdmin
    {
        public int Id { get; set; }

        [Display(Name = "وضعیت")]
        public string OrderStatus { get; set; }

        [Display(Name = "تاریخ به‌روزرسانی")]
        public string OrderDate { get; set; }

        [Display(Name = "کاربر")]
        public string  Username { get; set; }
    }
}
