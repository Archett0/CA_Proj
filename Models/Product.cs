using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CA_Proj.Models
{
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("product_description")]
        public string ProductDescription { get; set; }

        [Column("product_image")]
        public string ProductImage { get; set; }

        [Column("product_price")]
        public double ProductPrice { get; set; }

        [Column("product_download_link")]
        public string ProductDownloadLink { get; set; }

        [Column("product_overall_rating")]
        public double ProductOverallRating { get; set; }

        [Column("product_keywords")]
        public string ProductKeywords { get; set; }

        [Column("product_quantity_sold")]
        public int ProductQuantitySold { get; set; }

    }
}