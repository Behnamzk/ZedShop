using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class PostWeight
    {
        [Key]
        public int Id { get; set; }

        [AllowNull]
        public string? UntilWeightName { get; set; }

        [Required]
        public int UntilWeightNumber { get; set; }

        [Required]
        public double EffectOnPrice { get; set; }

        [ForeignKey("PostType")]
        public int PostTypeId { get; set; }
        public PostType PostType { get; set; }
    }
}
