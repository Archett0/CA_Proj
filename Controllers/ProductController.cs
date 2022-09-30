using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public IActionResult Index([FromRoute]string id)
        {
            
            var username=HttpContext.Session.GetString("username");
            ViewBag.Username=username;

            var sql = "SELECT* FROM product ";
            List<Product> allProducts = ProductData.Query(sql);
            ViewData["allProducts"] = allProducts;
                
            if (string.IsNullOrEmpty(username)) {//haven't login
                ViewBag.IsLogin=false;
            }
            else//already login
            {
                ViewBag.IsLogin = true;
            }
          
            int page=0;
            if (id == "01"||id==null)
            {
                page = 0;
                ViewData["page"] = page;
                sql = "SELECT* FROM product LIMIT 9";
                List<Product> products = ProductData.Query(sql);
                ViewData["products"] = products;
            }
            if (id == "02")
            {
                page = 1;
                ViewData["page"] = page;
                sql = "SELECT* FROM product LIMIT 9 OFFSET 9";
                List<Product> products = ProductData.Query(sql);
                ViewData["products"] = products;
            }
            if (id == "03")
            {
                page = 2;
                ViewData["page"] = page;
                sql = "SELECT* FROM product LIMIT 9 OFFSET 18";
                List<Product> products = ProductData.Query(sql);
                ViewData["products"] = products;
            }
            if (id == "04")
            {
                page = 3;
                ViewData["page"] = page;
                sql = "SELECT* FROM product LIMIT 9 OFFSET 27";
                List<Product> products = ProductData.Query(sql);
                ViewData["products"] = products;
            }

            return View();
        }

        
    }
}
