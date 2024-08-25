using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Web.Areas.Admin.Models.CategoryViewModel
{
    public class CategoryViewModelAdmin
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsRoot { get; set; } = true;

        public Category? Parent { get; set; }

    }
}
