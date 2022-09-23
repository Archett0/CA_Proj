using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CA_Proj.Models
{
    public class ProductActivationCode
    {
        
        [Column("activation_code")]
        public string Activation_code { get; set; }

        [Key]
        [Column("product_id")]
        public int Product_id { get; set; }

        [Column("code_status")]
        public short Code_status { get; set; }
    }
}
