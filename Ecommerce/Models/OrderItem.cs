using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class OrderItem
    {
        [Key]
        [Column(Order = 1)]
        public long Id { get; set; }
        [ForeignKey("Id")]
        public virtual Order Order { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ItemNumber { get; set; }

        public virtual Book Book{ get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Total { get; set; }
    }
}