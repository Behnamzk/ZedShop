using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZedShop.Web.Areas.Admin.Models.CommentViewModel
{
    public class CommentViewModelAdmin
    {
        [DisplayName("شناسه نظر")]

        public int Id { get; set; }

        [DisplayName("متن نظر")]
        public string CommentText { get; set; } = string.Empty;

        public int UserId { get; set; }

        [DisplayName("نام کاربر")]

        public string UserName { get; set; } = string.Empty;

        public int ProductId { get; set; }

        [DisplayName("نام کالا")]
        public string ProductName { get; set; } = string.Empty;


        [DisplayName("تاریخ نظر")]
        public string CommentDate { get; set; } = string.Empty;

        [DisplayName("نمایش")]
        public bool IsShow { get; set; } = false;


    }
}
