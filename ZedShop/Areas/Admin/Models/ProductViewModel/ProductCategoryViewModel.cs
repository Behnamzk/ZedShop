using System.ComponentModel.DataAnnotations;

namespace ZedShop.Web.Areas.Admin.Models.ProductViewModel
{
    public class ProductCategoryViewModelAdmin
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string  CategoryText { get; set; } = string.Empty;

        public List<CategoryViewModelAdmin> ProdcutCategories { get; set; } = new List<CategoryViewModelAdmin>();
    }

    public class CategoryViewModelAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
