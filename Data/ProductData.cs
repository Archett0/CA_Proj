using CA_Proj.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CA_Proj.Data
{
    public class ProductData : Data
    {
        public static List<Product> Query(string sql)
        {
            List<Product> products = new List<Product>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = (int)reader["product_id"],
                        ProductName = (string)reader["product_name"],
                        ProductDescription = (string)reader["product_description"],
                        ProductImage = (string)reader["product_image"],
                        ProductPrice = (double)reader["product_price"],
                        ProductDownloadLink = (string)reader["product_download_link"],
                        ProductOverallRating = (double)reader["product_overall_rating"],
                        // Unable to cast object of type 'System.DBNull' to type 'System.String'
                        //ProductKeywords = (string)reader["product_keywords"],
                        ProductQuantitySold = (int)reader["product_quantity_sold"],
                    };
                    products.Add(product);
                }
            }
            return products;
        }
        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT *
                                FROM product";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = (int)reader["product_id"],
                        ProductName = (string)reader["product_name"],
                        ProductDescription = (string)reader["product_description"],
                        ProductImage = (string)reader["product_image"],
                        ProductPrice = (double)reader["product_price"],
                        ProductDownloadLink = (string)reader["product_download_link"],
                        ProductOverallRating = (double)reader["product_overall_rating"],
                        //ProductKeywords = (string)reader["product_keywords"],
                        ProductQuantitySold = (int)reader["product_quantity_sold"],
                    };
                    products.Add(product);
                }
            }

            return products;
        }


        public static Product GetProductDetailsByProductId(int productId)
        {
            Product product = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql = @"SELECT * 
                             FROM product 
                             WHERE product_id =" + productId;

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product()
                    {
                        ProductId = (int)reader["product_id"],
                        ProductName = (string)reader["product_name"],
                        ProductDescription = (string)reader["product_description"],
                        ProductImage = (string)reader["product_image"],
                        ProductPrice = (double)reader["product_price"],
                        ProductDownloadLink = (string)reader["product_download_link"],
                        ProductOverallRating = (double)reader["product_overall_rating"],
                        //ProductKeywords = (string)reader["product_keywords"],
                        ProductQuantitySold = (int)reader["product_quantity_sold"],
                    };
                };
            }

            return product;
        }
    }
}

