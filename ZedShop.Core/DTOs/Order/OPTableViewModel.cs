using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Order
{
    public class OPTableViewModel
    {
        public List<OrderProductViewModel> items {  get; set; }

        public double total_price { get; set; } = 0;
    }
}
