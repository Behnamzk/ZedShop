using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class PostBox
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string BoxName { get; set; }

        [Required]
        public double EffectOnPrice { get; set; }

        [Required]
        public double BoxPrice { get; set; }

        [Required]
        public float Width { get; set; }

        [Required]
        public float Height { get; set; }

        [Required]
        public float Lenght { get; set; }

        [ForeignKey("PostType")]
        public int PostTypeId { get; set; }
        public PostType PostType { get; set; }
    }
}
