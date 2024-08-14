using System.ComponentModel.DataAnnotations;

namespace ZedShop.Web.Areas.Admin.Models.UserViewModel
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "نام")]

        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Display(Name ="نام نمایشی")]
        public string DisplayName { get; set; } = string.Empty;
    }
}
