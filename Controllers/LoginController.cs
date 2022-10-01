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
            System.Console.WriteLine("islogining");
            string username = form["username"];
            string password = form["password"];

            //var sql = $"SELECT * FROM `user` WHERE username=@p0 AND `password`=@p1";
            var query = _context.Users.AsQueryable();
            var result = query.Where(x => x.Username == username && x.Password == password);
            if (result.Count()==1)
            {
                var user = result.First();
                HttpContext.Session.SetString("username",username);
                HttpContext.Session.SetObject<int>("userid", user.Id);
                /*return Json(new
                {
                    success = true
                });*/
                // send user to Cart page
                return RedirectToAction("Index", "Product");

            }
            return Json(new
            {
                success = false,
                message= "You have entered an incorrect user name or password. Please try again."
            }) ;
        }

    }
}
