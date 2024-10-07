using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ZedShop.Core.DTOs.Product;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Web.Areas.Admin.Models.OrdersViewModel
{
    public class EditOrderViewModel
    {

        public double TotalPrice { get; set; } = 0;
        public double ProductsPrice { get; set; } = 0;

        public Order order { get; set; }
        public List<OrderDetailProductsViewModel> Products { get; set; }

    }
}
