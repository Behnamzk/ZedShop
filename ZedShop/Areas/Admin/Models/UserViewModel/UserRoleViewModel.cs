using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZedShop.Web.Areas.Admin.Models.UserViewModel
{
    public class UserRoleViewModel
    {
        public int UserId { get; set; }

        [DisplayName("نام کاربری")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserName { get; set; } = string.Empty;

        public int RoleId { get; set; }

        [DisplayName("نقش کاربری")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string RoleName { get; set; } = string.Empty;

        public int SelectedRoleId { get; set; }

    }
}
