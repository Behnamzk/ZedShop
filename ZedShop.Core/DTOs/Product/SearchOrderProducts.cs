using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Product
{
    public class SearchOrderProducts
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SearchOrderProducts(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
