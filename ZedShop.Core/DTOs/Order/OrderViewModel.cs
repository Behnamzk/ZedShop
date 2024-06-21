using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Order
{
    public class OrderViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("تعداد کالا شما")]
        [EmailAddress(ErrorMessage = "تعداد وارد شده معتبر نمی‌باشد!")]
        public int Count { get; set; }
    }
}
