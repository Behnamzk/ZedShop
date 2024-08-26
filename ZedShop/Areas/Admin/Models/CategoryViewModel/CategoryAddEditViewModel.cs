using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ZedShop.DataLayer.Entities;
using System.ComponentModel;

namespace ZedShop.Web.Areas.Admin.Models.CategoryViewModel
{
    public class CategoryAddEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("نام دسته‌بندی")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Name { get; set; } = string.Empty;

        public List<CategoryViewModelTemp> ParentCategory { get; set; } = new List<CategoryViewModelTemp>();

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayName("والد")]
        public bool IsRoot { get; set; } = true;
    }

    public class CategoryViewModelTemp
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = false;
    }
}
