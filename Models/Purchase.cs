using CA_Proj.Controllers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CA_Proj.Models
{
    public class Purchase
    {
        
        public Purchase(bool Form_Type)
        {
            PurchaseId = 0;
            UserId = 0;
            CreateTime= DateTime.Now;
            PaymentTime = null;
            ShippingTime = null;
            TotalPrice = 0;
            IsCart = 1;
            Status = 1;//表示暂时存储
        }

        public Purchase(int id)
        {
            PurchaseId = id;
            UserId = 0;
            CreateTime = DateTime.Now;
            PaymentTime = null;
            ShippingTime = null;
            TotalPrice = 0;
            IsCart = 1;
            Status = 1;//表示暂时存储
        }


        public Purchase() { }
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
