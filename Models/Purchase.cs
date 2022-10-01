using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CA_Proj.Models
{
    public class Purchase
    {
        [Key]
        [Column("purchase_id")]
        public int PurchaseId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("purchase_create_time")]
        public DateTime CreateTime { get; set; }

        [Column("purchase_payment_time")]
        public DateTime? PaymentTime { get; set; }

        [Column("purchase_shipping_time")]
        public DateTime? ShippingTime { get; set; }

        [Column("purchase_total_price")]
        public double TotalPrice { get; set; }
        [Column("purchase_is_cart")]
        public int IsCart { get; set; }
        [Column("purchase_status")]
        public short Status { get; set; }
    }
}
