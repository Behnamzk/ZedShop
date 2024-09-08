using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.DTOs.Order
{
    public class OrderPurchaseViewModel
    {
        public int Id { get; set; }

        [MaxLength(1000)]
        public string Address { get; set; } = string.Empty;

        public int? CityId { get; set; }

        public string City { get; set; } = string.Empty;

        public int? ProvinceId { get; set; }

        public string Province { get; set; } = string.Empty;

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
