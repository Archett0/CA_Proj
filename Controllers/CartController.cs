using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CA_Proj.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using CA_Proj.Models;
using Microsoft.AspNetCore.Http;

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
            var userId = HttpContext.Session.GetObject<int>("userid");

            var cartUpToDate = HttpContext.Session.GetObject<bool>("cart_upto_date");
            //System.Console.WriteLine("string now:{0},{1}",userID,cart_upto_date);
            if ((cartUpToDate == false) && userId != 0)
            {
                var query = _context.Purchases.AsQueryable();
                //var test = _context.purchases;
                query = query.Where(c => c.UserId == userId);
                query = query.Where(c => c.IsCart == 1);
                //System.Console.WriteLine("please give me some information about how i down");
                System.Console.WriteLine(query.Count());
                if (query.Count() != 1)
                {
                    return View();
                }
                else
                {
                    var purchase = query.First();
                    var id = purchase.PurchaseId;
                    //System.Console.WriteLine("PurchaseId={0}",id);
                    var ret = await _context.PurchaseProducts.Where(c => c.PurchaseId == id).Include(p => p.Product).ToListAsync();
                    HttpContext.Session.SetObject("cart", ret);
                    HttpContext.Session.SetObject("cart_upto_date", true);
                    //System.Console.WriteLine("here we are");
                    return View(ret);
                }
            }
            else
            {
                //var cart = HttpContext.Session.GetObject<List<Models.PurchaseProduct>>("cart");
                return View();
            }
        }

        public IActionResult CheckOut()
        {
            //默认登录状态
            //将购物车订单变成非购物车订单，同时创建新的购物车空订单与用户关联，实现原购物车变为历史订单，新购物车清空。
            return Json(new
            {
     
                message = "failed."
            });
        }
        public IActionResult CheckoutAndLogin(IFormCollection form)
        {
            //此处游客点击结账，登陆后temp购物车直接变成从购物车状态变成订单状态并且与userID关联
            //创建新的temp购物车（userid=0就是临时购物车）
            return Json(new
            {
               
                message = "failed."
            });
        }
    }
}
