using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CA_Proj.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using CA_Proj.Models;

namespace CA_Proj.Controllers
{
    public class CartController : Controller
    {
        private SystemContext _context;

        public CartController(SystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int userID=HttpContext.Session.GetObject<int>("userid");
            var cart_upto_date=HttpContext.Session.GetObject<bool>("cart_upto_date");
            //System.Console.WriteLine("string now:{0},{1}",userID,cart_upto_date);
            if ((cart_upto_date==false)&&userID!=0)
            {
                var query = _context.Purchases.AsQueryable();
                //var test = _context.purchases;
                query = query.Where(c => c.User_id == userID);
                query = query.Where(c => c.Is_cart == 1);
                //System.Console.WriteLine("please give me some information about how i down");
                System.Console.WriteLine(query.Count());
                if (query.Count() != 1) 
                { 
                    return View(); 
                }
                else
                {
                    var purchase = query.First();
                    var id = purchase.Purchase_id;
                    //System.Console.WriteLine("PurchaseId={0}",id);
                    var ret = _context.PurchaseProducts.Where(c => c.PurchaseId == id).Include(p => p.Product).ToListAsync();
                    HttpContext.Session.SetObject("cart", ret.Result);
                    HttpContext.Session.SetObject("cart_upto_date", true);
                    //System.Console.WriteLine("here we are");
                    return View(ret.Result);
                }
            }
            else
            {
                var cart = HttpContext.Session.GetObject<List<Models.PurchaseProduct>>("cart");
                return View(cart);
            }
        }

        public async Task<IActionResult> History()
        {
            int userID = HttpContext.Session.GetObject<int>("userid");
            if (userID != 0)
            {
                var query = _context.Purchases.AsQueryable();
                query = query.Where(c => c.User_id == userID);
                query = query.Where(c => c.Is_cart == 0);
                System.Console.WriteLine(query.Count());
                if (query.Count() == 0)
                {
                    return View();
                }
                else
                {
                    List<PurchaseProduct> result=null;
                    foreach (var product in query)
                    {
                        var id = product.Purchase_id;
                        var ret = _context.PurchaseProducts.Where(c => c.PurchaseId == id).Include(p => p.Product).ToListAsync();
                        result=result.Concat(ret.Result).ToList();
                    }
                    return View(result);
                }
            }
            else
            {
                return View();
            }
        }
    }
}
