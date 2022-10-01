using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CA_Proj.Models
{
    public class ProductActivationCode
    {
        
        [Key]
        [Column("activation_code")]
        public string ActivationCode { get; set; }

        [Column("purchase_product_id")]
        public int PurchaseProductId { get; set; }

        [Column("code_status")]
        public short CodeStatus { get; set; }

    }
}
