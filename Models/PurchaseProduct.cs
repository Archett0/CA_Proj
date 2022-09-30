using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CA_Proj.Models
{
    public class PurchaseProduct
    {
        [Column("purchase_id")]
        public int PurchaseId { get; set; }

        public Purchase Purchase { get; set; }

        [Column("product_id")]
        public int ProductId{ get; set; }

        public Product Product { get; set; }

        [Column("product_quantity")]
        public int ProductQuantity { get; set; }

        [Column("customer_rating")]
        public double CustomerRating { get; set; }

        [Column("customer_comment")]
        public string CustomerComment { get; set; }

        
    }
}
