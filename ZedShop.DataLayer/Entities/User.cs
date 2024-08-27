using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "آدرس ایمیل معتیر نیست")]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("کد فعال‌سازی")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string ActiveCode { get; set; }

        [DisplayName("وضعیت")]
        public bool IsActive { get; set; }

        [DisplayName("مسدود")]
        public bool IsBan { get; set; }

        [DisplayName("آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string UserAvatar { get; set; }

        [DisplayName("تاریخ ثبت‌نام")]
        public DateTime RegisterDate { get; set; }

        [Required]
        public GenderTypes gender { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
        public bool IsDelete { get; set; } = false;

        public Admin? Admin { get; set; }

        public Wallet? Wallet { get; set; }

        public ICollection<Order> Orders { get; set; }

		public ICollection<Comment> Comments { get; set; }

	}

	public enum GenderTypes { مرد, زن, سایر }
}
