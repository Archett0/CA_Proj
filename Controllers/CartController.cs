using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CA_Proj.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using CA_Proj.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace CA_Proj.Controllers
{
    public class CartController : Controller
    {
        private SystemContext _context;


        public CartController(SystemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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
            //var userId = HttpContext.Session.GetObject<int>("userid");

            //var cartUpToDate = HttpContext.Session.GetObject<bool>("cart_upto_date");
            //System.Console.WriteLine("string now:{0},{1}",userID,cart_upto_date);
            //if ((cartUpToDate == false) && userId != 0)
            //{
            //    var query = _context.Purchases.AsQueryable();
            //var test = _context.purchases;
            //    query = query.Where(c => c.UserId == userId && c.IsCart == 1);
            //System.Console.WriteLine("please give me some information about how i down");
            //    System.Console.WriteLine(query.Count());
            //    if (query.Count() != 1)
            //    {
            //        return View();
            //    }
            //    else
            //    {
            //        var purchase = query.First();
            //        var id = purchase.PurchaseId;
            //       //System.Console.WriteLine("PurchaseId={0}",id);
            //        var ret = await _context.PurchaseProducts.Where(c => c.PurchaseId == id).Include(p => p.Product).ToListAsync();
            //        HttpContext.Session.SetObject("cart", ret);
            //        HttpContext.Session.SetObject("cart_upto_date", true);
            //        //System.Console.WriteLine("here we are");
            //        return View(ret);
            //    }
            //}
            //else
            //{
            // System.Console.WriteLine("23333");
            ViewData["Discount"] = TempData["Discount"] ?? null;
            var cart = HttpContext.Session.GetObject<List<PurchaseProduct>>("cart") ?? new List<PurchaseProduct>();
            return View(cart);
            //}
        }

        public IActionResult CheckOut()
        {
            //默认登录状态
            //将购物车订单变成非购物车订单，同时创建新的购物车空订单与用户关联，实现原购物车变为历史订单，新购物车清空。
            var cartProduct = HttpContext.Session.GetObject<List<PurchaseProduct>>("cart");
            int purchaseId = Convert.ToInt32(HttpContext.Session.GetString("purchaseId"));
            var cart = new Purchase(purchaseId);
            cart.CreateTime = DateTime.Now;
            cart.IsCart = 0;
            cart.Status = 0;
            cart.UserId = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            //_context.Purchases.Add(cart);
            var subtotal = 0.0;
            foreach (var product in cartProduct)
            {
                int num = product.ProductQuantity;
                product.Id = _context.PurchaseProducts.OrderBy(pp => pp.Id).Last().Id + 1;
                product.Purchase = null;
                product.PurchaseId = cart.PurchaseId;
                Console.WriteLine("purchaseProductId:{0}", product.PurchaseId);
                subtotal += product.ProductQuantity * product.Product.ProductPrice;
                product.Product = null;
                _context.PurchaseProducts.Add(product);
                _context.SaveChanges();
                for (int i = 1; i <= num; i++)
                {
                    var activation_code = CreateAndCheckCode();
                    product.ActivationCodes.Add(activation_code);
                    _context.ProductActivationCodes.Add(new ProductActivationCode(activation_code, product.Id, 0));
                    _context.SaveChanges();
                }

            }
            //_context.Cart.IsCart = 0;
            _context.Purchases.Update(cart);
            _context.SaveChanges();
            System.Console.WriteLine(_context.ProductActivationCodes.Count().ToString() + " " + _context.PurchaseProducts.Count().ToString());

            HttpContext.Session.SetObject("cart_upto_date", true);
            HttpContext.Session.SetObject("cart", new List<PurchaseProduct>());
            //_context.Cart = new Purchase(true);

            return RedirectToAction("index", "Order");
        }

        public IActionResult Discount(string coupon)
        {
            if(coupon == null) return RedirectToAction("Index", "Cart");
            // get discount for user. if the coupon is not valid, alert the user
            var discount = _context.Coupons.Where(c => c.CouponCode.Equals(coupon.Trim().ToLower()))
                .Select(c => c.CouponDiscount).FirstOrDefault();

            TempData["Discount"] = !discount.Equals(0) ? discount.ToString() : -1;
            return RedirectToAction("Index", "Cart");
        }

        private string CreateAndCheckCode()
        {
            Random rand = new Random(~unchecked((int)DateTime.Now.Ticks));
            string result = null;
            var query = _context.ProductActivationCodes.AsQueryable();
            while (result == null || query.Where(x => x.ActivationCode == result).Count() != 0)
            {
                for (int i = 0; i < 32; i++)
                {
                    if (i % 8 == 0 && i > 0) result += '-';
                    else
                    {
                        int rnd = rand.Next(0, 26);
                        result += (char)((int)'a' + rnd);
                    }
                }
            }
            Console.WriteLine("create a activation code:" + result);
            return result;
        }
    }
}
