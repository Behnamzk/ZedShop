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
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; } = string.Empty;

        [AllowNull]
        [ForeignKey("Category")]
        public int? ParentId { get; set; }

        [AllowNull]
        public Category? Parent { get; set; }

        public bool IsRoot { get; set; } = true;
		public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
    }
}
