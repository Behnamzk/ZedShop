using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string NameCode { get; set; }

        [AllowNull]
        [MaxLength(300)]

        public string? Description { get; set; }

        [AllowNull]
        public DateTime? StartDate { get; set; }

        [AllowNull]
        public DateTime? EndDate { get; set; }

        [AllowNull]
        public int? Count { get; set; }

        [Required]
        public float Value { get; set; }

        [Required]
        public bool IsActive { get; set; } = false;

        [Required]
        public bool IsShow { get; set; } = false;
    }
}
