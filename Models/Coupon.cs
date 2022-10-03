using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CA_Proj.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        [Column("coupon_code")]
        public string CouponCode { get; set; }

        [Column("coupon_discount")]
        public double CouponDiscount { get; set; }
    }
}