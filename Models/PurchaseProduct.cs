using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CA_Proj.Models
{
    public class PurchaseProduct
    {



        public PurchaseProduct(int purchaseId, Purchase purchase, int productId, Product product, int productQuantity, double customerRating, string customerComment, List<string> activationCodes)
        {
            PurchaseId = purchaseId;
            Purchase = purchase;
            ProductId = productId;
            Product = product;
            ProductQuantity = productQuantity;
            CustomerRating = customerRating;
            CustomerComment = customerComment;
            ActivationCodes = activationCodes;
            Id = 0;
        }

        public PurchaseProduct() {
            ActivationCodes = new List<string>();
        }

        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("purchase_id")]
        public int PurchaseId { get; set; }

        public Purchase Purchase { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Column("product_quantity")]
        public int ProductQuantity { get; set; }

        [Column("customer_rating")]
        public double CustomerRating { get; set; }

        [Column("customer_comment")]
        public string CustomerComment { get; set; }

        [NotMapped]
        public List<string> ActivationCodes { get; set; }
    }
}
