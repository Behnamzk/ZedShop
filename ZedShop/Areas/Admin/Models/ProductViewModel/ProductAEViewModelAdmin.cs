using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZedShop.Web.Areas.Admin.Models.ProductViewModel
{
    public class ProductAEViewModelAdmin
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("نام کالا")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("توضیحات کالا")]
        [MaxLength(400, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Description { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("تعداد کالا")]
        public int Count { get; set; }

        public bool IsActivate { get; set; }

        [DisplayName("تخفیف کالا")]
        public double DisCount { get; set; }

        public bool IsShow { get; set; }

        public bool IsDelete { get; set; } = false;

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("تاریخ خرید کالا")]
        public string BuyDateSTR { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("قیمت فروش کالا")]
        public double SellPrice { get; set; }

        public string ProductImageName { get; set; }

        [DisplayName("تصویر جدید کالا")]
        public IFormFile? ProductImageFile { get; set; }


    }
}
