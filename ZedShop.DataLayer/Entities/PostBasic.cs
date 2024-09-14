using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class PostBasic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double BasePrice { get; set; }

        [ForeignKey("PostType")]
        public int PostTypeId { get; set; }

        public PostType PostType { get; set; }

    }

    //public enum PostTypes { پیشتاز, سفارشی, عادی, اکسپرس, دوقبضه, سایر}
}
