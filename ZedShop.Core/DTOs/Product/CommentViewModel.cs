using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.Core.DTOs.Product
{
    public class CommentViewModel
    {
        public string Content { get; set; } = String.Empty;
        public int UserId { get; set; }
        public int PoroductId { get; set; }
    }
}
