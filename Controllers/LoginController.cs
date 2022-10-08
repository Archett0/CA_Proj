using System.Threading.Tasks;
using CA_Proj.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using CA_Proj.Models;

namespace CA_Proj.Controllers
{ 
    public class LoginController : Controller
    {
        private SystemContext _context;
        public LoginController(SystemContext context)
        {
            _context = context;
        }
        public IActionResult Login(IFormCollection form)
        {
            // data from client
            string username = form["username"];
            if (username == "default_customer")
            {
                return Json(new
                {
                    success = false,
                    message = "You have entered an incorrect user name or password. Please try again."
                });
            }
            string password = form["password"];
            var query = _context.Users.AsQueryable();
            var result = query.Where(x => x.Username == username && x.Password == password);
            if (result.Count()==1)
            {
                var user = result.First();
                System.Console.WriteLine(user.ToString());
                HttpContext.Session.SetString("username",user.Nickname);
                HttpContext.Session.SetString("userid", user.Id.ToString());
                //var cart = _context.Cart;
                //cart.UserId = user.Id;
                var cartProducts = HttpContext.Session.GetObject<List<PurchaseProduct>>("cart");
                if (cartProducts == null) {
                    Console.WriteLine("cart is null!");
                    cartProducts = new List<PurchaseProduct>(); 
                }
                var _query = _context.Purchases.AsQueryable().Where(x => (x.IsCart == 1 && x.UserId == user.Id));
                var id=0;
                if (_query.Count() == 0)
                {
                    //storageCart = cart;
                    id = (_context.Purchases.Count() + 1);
                    var purchase=new Purchase(id)
                    {
                        UserId = user.Id
                    };
                    //storageCart.PurchaseId = _context.Purchases.Count() + 1;
                    _context.Purchases.Add(purchase);
                    _context.SaveChanges();
                }
                else id = _query.First().PurchaseId;
                var storageCartProducts=_context.PurchaseProducts.AsQueryable().Where(x=>x.PurchaseId==id).Include(pp => pp.Product).ToList();
                if(storageCartProducts.Count() != 0)
                foreach(var product in storageCartProducts)
                {
                    product.Id = 0;
                        
                    cartProducts.Add(product);
                }
                foreach(var product in cartProducts)
                {
                    product.PurchaseId = id;
                }
                //_context.Cart =storageCart;
                HttpContext.Session.SetObject("cart",cartProducts);
                HttpContext.Session.SetString("purchaseId", id.ToString());

                return RedirectToAction("Index", "Product");

            }
            return Json(new
            {
                success = false,
                message= "You have entered an incorrect user name or password. Please try again."
            }) ;
        }





        public IActionResult Login2(IFormCollection form)
        {
            // data from client
            string username = form["username"];
            if (username == "default_customer")
            {
                return Json(new
                {
                    success = false,
                    message = "You have entered an incorrect user name or password. Please try again."
                });
            }
            string password = form["password"];
            var query = _context.Users.AsQueryable();
            var result = query.Where(x => x.Username == username && x.Password == password);
            if (result.Count() == 1)
            {
                var user = result.First();
                System.Console.WriteLine(user.ToString());
                HttpContext.Session.SetString("username", user.Nickname);
                HttpContext.Session.SetString("userid", user.Id.ToString());
                //var cart = _context.Cart;
                //cart.UserId = user.Id;
                var cartProducts = HttpContext.Session.GetObject<List<PurchaseProduct>>("cart");
                if (cartProducts == null)
                {
                    Console.WriteLine("cart is null!");
                    cartProducts = new List<PurchaseProduct>();
                }
                var _query = _context.Purchases.AsQueryable().Where(x => (x.IsCart == 1 && x.UserId == user.Id));
                var id = 0;
                if (_query.Count() == 0)
                {
                    //storageCart = cart;
                    id = (_context.Purchases.Count() + 1);
                    var purchase = new Purchase(id)
                    {
                        UserId = user.Id
                    };
                    //storageCart.PurchaseId = _context.Purchases.Count() + 1;
                    _context.Purchases.Add(purchase);
                    _context.SaveChanges();
                }
                else id = _query.First().PurchaseId;
                var storageCartProducts = _context.PurchaseProducts.AsQueryable().Where(x => x.PurchaseId == id).Include(pp => pp.Product).ToList();
                if (storageCartProducts.Count() != 0)
                    foreach (var product in storageCartProducts)
                    {
                        product.Id = 0;

                        cartProducts.Add(product);
                    }
                foreach (var product in cartProducts)
                {
                    product.PurchaseId = id;
                }
                //_context.Cart =storageCart;
                HttpContext.Session.SetObject("cart", cartProducts);
                HttpContext.Session.SetString("purchaseId", id.ToString());

                return RedirectToAction("Index", "Cart");

            }
            return Json(new
            {
                success = false,
                message = "You have entered an incorrect user name or password. Please try again."
            });
        }
    }
}
