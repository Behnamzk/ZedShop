using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class PostDistance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public double EffectOnPrice { get; set; }

        [ForeignKey("PostType")]
        public int PostTypeId { get; set; }

        public PostType PostType { get; set; }
    }
}
