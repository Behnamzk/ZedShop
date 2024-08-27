using System.ComponentModel;

namespace ZedShop.Web.Areas.Admin.Models.OpinionViewModel
{
    public class OpinionViewModelAdmin
    {
        [DisplayName("شناسه دیدگاه")]

        public int Id { get; set; }

        [DisplayName("متن دیدگاه")]
        public string OpinionText { get; set; } = string.Empty;

        public int UserId { get; set; }

        [DisplayName("نام کاربر")]
        public string UserName { get; set; } = string.Empty;

        [DisplayName("تاریخ دیدگاه")]
        public string OpinionDate { get; set; } = string.Empty;

        [DisplayName("نمایش")]
        public bool IsShow { get; set; } = false;

        [DisplayName("مسدود")]
        public bool IsBan { get; set; } = false;

    }
}
