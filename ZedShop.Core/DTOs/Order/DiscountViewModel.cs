using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Order
{
    public class DiscountViewModel
    {
        public int Id { get; set; }

        [Display(Name = "مقدار")]

        public double Value { get; set; }

        [Display(Name = "کد")]
        public string NameCode { get; set; } = string.Empty;

        [Display(Name = "نام")]

        public string DisplayName { get; set; } = string.Empty;
    }
}
