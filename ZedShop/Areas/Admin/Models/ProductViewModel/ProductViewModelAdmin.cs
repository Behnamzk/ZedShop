using ZedShop.DataLayer.Entities;

namespace ZedShop.Web.Areas.Admin.Models.ProductViewModel
{
    public class ProductViewModelAdmin
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsActivate { get; set; }

        public bool IsShow { get; set; }

        public double SellPrice { get; set; }

        public int Count { get; set; }

        public string BuyDate { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

        public bool IsDelete { get; set; }

    }
}
