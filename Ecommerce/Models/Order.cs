using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        [DisplayName("Order Status")]
        public int OrderStatus { get; set; }

        [Required]
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Date Finished")]
        public DateTime? DateFinished { get; set; }

        [Required]
        public double Total { get; set; }

        [DisplayName("Tracking Number")]
        public string TrackingNumber { get; set; }

        public string OrderLog { get; set; }

        [Required]
        [DisplayName("Billing ddress")]
        public string BillingAddress { get; set; }
        [Required]
        [DisplayName("Shipping Address")]
        public string ShippingAddress { get; set; }
    }
}