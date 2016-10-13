using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Cart
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ItemNumber { get; set; }

        public virtual Book Book { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateTimeAdded { get; set; }
        [NotMapped]
        public double Total
        {
            get
            {
                if (Book == null)
                {
                    return 0;
                }
                else
                {
                    return Quantity * Book.Price;
                }
            }
        }
    }
}