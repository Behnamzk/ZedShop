using System;
using System.Collections.Generic;
using System.Text;

namespace ZedShop.Core.Convertors
{
    public class FixText
    {
        public static string FixEmail(string Email)
        {
            return Email.Trim().ToLower();
        }
    }
}
