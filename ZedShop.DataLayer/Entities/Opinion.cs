using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class Opinion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string OpinionText { get; set; } = string.Empty;

        [ForeignKey("User")]
        public int UserId { get; set; }

        public bool IsBan { get; set; }

        public short OpinionRate { get; set; }

        public DateTime OpinionDate { get; set; }

        public User User { get; set; }

    }
}
