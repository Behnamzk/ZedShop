using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Home
{
    public class OpinionViewModel
    {
        public string Content { get; set; } = String.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = String.Empty;
        public DateTime Date { get; set; }
        public string UserAvatar { get; set; } = String.Empty;
    }
}
