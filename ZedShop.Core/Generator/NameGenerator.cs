using System;
using System.Collections.Generic;
using System.Text;

namespace ZedShop.Core.Generator
{
    public class NameGenerator
    {

        // GenerateUniqueCode return unique code for user activition code 
        public static string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
