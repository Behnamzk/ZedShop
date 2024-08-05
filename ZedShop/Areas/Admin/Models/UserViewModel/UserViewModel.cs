using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Web.Areas.Admin.Models.UserViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("نام کاربری")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("کلمه عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Password { get; set; } = string.Empty;

        [DisplayName("آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserAvatar { get; set; } = string.Empty;

        public IFormFile? ProfileFile { get; set; }


        public GenderTypes Gender { get; set; }

        public bool IsActive { get; set; } = false;

        public bool IsBan { get; set; } = false;



    }
}
