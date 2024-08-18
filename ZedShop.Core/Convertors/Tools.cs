using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.Convertors
{
    public class Tools
    {
        public static string ConvertPersianToEnglishNumbers(string input)
        {
            var persianNumbers = new[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
            var englishNumbers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            return new string(input.Select(c => persianNumbers.Contains(c) ? englishNumbers[Array.IndexOf(persianNumbers, c)] : c).ToArray());
        }

    }
}
