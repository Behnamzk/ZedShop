using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Order
{
    public class ProvinceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<CityViewModel> Cities { get; set; }
    }
}
