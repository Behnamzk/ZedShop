using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace ZedShop.Core.DTOs
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("نام کاربری")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        public string Email { get; set; }

        [DisplayName("کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Password { get; set; }

        [DisplayName("تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [Compare("Password", ErrorMessage ="کلمه عبور مغایرت دارند")]
        public string RePassword { get; set; }

        public string Gender {  get; set; }
    }


    public class LoginViewModel
    {

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        public string Email { get; set; }

        [DisplayName("کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Password { get; set; }

        [DisplayName("مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
