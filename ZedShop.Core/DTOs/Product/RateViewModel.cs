using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Product
{
    public class RateViewModel
    {
        public int Rate { get; set; } = 0;
        public int UserId { get; set; }
        public int PoroductId { get; set; }
    }
}
