namespace CA_Proj.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDownloadLink { get; set; }
        public double ProductOverallRating { get; set; }
        public string ProductKeywords { get; set; }
        public int ProductQuantitySold { get; set; }

    }
}