using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{
    public class RoleAccess
    {
        [Key]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Key]
        [ForeignKey("Accesss")]
        public int AccessId { get; set; }

        public Role Role { get; set; }

        public Access Access { get; set; }
    }
}
