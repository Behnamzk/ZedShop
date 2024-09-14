using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedShop.DataLayer.Entities
{

    // TODO: Complete this part ...
    public class PostDelivery
    {
        public int Id { get; set; }

        public double TotalPrice { get; set; }


        [ForeignKey("PostBox")]
        [AllowNull]
        public int? PostBoxId { get; set; }

        [AllowNull]
        public PostBox? PostBox { get; set; }


        [ForeignKey("PostWeight")]
        [AllowNull]
        public int? PostWeightId { get; set; }

        [AllowNull]
        public PostWeight? PostWeight { get; set; }


        [ForeignKey("PostDistance")]
        [AllowNull]
        public int? PostDistanceId { get; set; }
        
        [AllowNull]
        public PostDistance? PostDistance { get; set; }

        
        [ForeignKey("PostBasic")]
        [AllowNull]
        public int? PostBasicId { get; set; }

        [AllowNull]
        public PostBasic? PostBasic { get; set; }

        // type , box size 
    }
}
