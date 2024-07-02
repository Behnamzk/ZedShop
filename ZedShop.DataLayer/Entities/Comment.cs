using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class Comment
    {
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(1000)]
		public string CommentText { get; set; }=string.Empty;

		[ForeignKey("User")]
		public int UserId { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }

		public DateTime CommentDate { get; set; }

		public User User { get; set; }

		public Product Product { get; set; }

	}
}
