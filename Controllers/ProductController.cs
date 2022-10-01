using System.Threading.Tasks;
using CA_Proj.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using CA_Proj.Models;
using System.Linq;

namespace CA_Proj.Controllers
{
    public class ProductController : Controller
    {
        private readonly SystemContext _context;
        //public static SystemContext Default_context;

        public ProductController(SystemContext context)
        {
            _context = context;
            //Default_context = context;
        }

        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("username");
            ViewBag.Username = username;
            if (string.IsNullOrEmpty(username))
            {//haven't login
                ViewBag.IsLogin = false;
            }
            else//already login
            {
                ViewBag.IsLogin = true;
            }
            return View(await _context.Products.ToListAsync());
        }

        public IActionResult Add()
        {
            //传入商品的id和登录用户userid添加purchase_product数据
            HttpContext.Session.SetObject("cart_upto_date", false);
            Console.WriteLine(Request.Query["id"]);
            int id = Convert.ToInt32(Request.Query["id"]);
            var cart = HttpContext.Session.GetObject<List<PurchaseProduct>>("cart");
            if (cart == null)
            {
                Console.WriteLine("cart is null!");
                cart = new List<PurchaseProduct>();
            }
            var product = _context.Products.AsQueryable().Where(x => x.ProductId == id).First();
            var query = cart.AsQueryable().Where(x => x.ProductId == id);
            if (query.Count() == 0) {
                var purchaseproduct = new PurchaseProduct(id, null, id, product, 1, 1.0, null,new List<string>());
                cart.Add(purchaseproduct);
            }
            else query.First().ProductQuantity += 1;
            HttpContext.Session.SetObject("cart", cart);

            return RedirectToAction("index", "cart");
        }
    }
}
