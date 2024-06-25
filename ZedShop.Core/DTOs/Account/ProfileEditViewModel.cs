using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;
using Microsoft.AspNetCore.Http;


namespace ZedShop.Core.DTOs.Account
{
    public class ProfileEditViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("نام کاربری")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; } = string.Empty;

        public string OldUserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی‌باشد")]
        public string Email { get; set; } = string.Empty;

        public GenderTypes Gender { get; set; }

        [DisplayName("آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserAvatar { get; set; } = String.Empty;

        public IFormFile? ProfileFile { get; set; }
    }
}
