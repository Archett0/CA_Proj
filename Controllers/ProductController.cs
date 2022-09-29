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
            var sql = "SELECT* FROM product LIMIT 9";
            ViewData["products"] = ProductData.Query(sql);
            //ViewBag.Messages= ProductData.Query(sql);
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

        
    }
}
