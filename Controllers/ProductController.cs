using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CA_Proj.Data;
using CA_Proj.Models;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;


namespace CA_Proj.Controllers
{
    public class ProductController : Controller
    {
        private readonly SystemContext _context;

        public ProductController(SystemContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index([FromRoute]string id)
        {
            var username=HttpContext.Session.GetString("username");
            ViewBag.Username=username;
            if (string.IsNullOrEmpty(username)) {
                ViewBag.IsLogin=false;
            }
            else
            {
                ViewBag.IsLogin = true;
            }
            /*var name = Request.Cookies["SessionId"];
            if (string.IsNullOrEmpty(name))//haven't login
            {
                return View(await _context.Products.ToListAsync());
            }
            else//already login
            {
                return RedirectToAction("Index", "Cart");
            }*/
            /*if (id == "01")
            {
                var sql="SELECT* FROM product LIMIT 9";
                ProductData
            }*/
           
            return View(await _context.Products.ToListAsync());
        }

        public static List<Product> GetProductsInDifferentPage(string sql)
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = new MySqlConnection("SystemContext"))
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
                        ProductKeywords = (string)reader["product_keywords"],
                        ProductQuantitySold = (int)reader["product_quantity_sold"],
                    };
                    products.Add(product);
                }
            }
            return products;
        }
    }
}
