using CA_Proj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using System;
using CA_Proj.Data;
using MySql.Data.MySqlClient;

namespace CA_Proj.Controllers
{ 
    public class LoginController : Controller
    {
        
        public IActionResult Login(IFormCollection form)
        {
            // data from client
            string username = form["username"];
            string password = form["password"];

            var sql = $"SELECT * FROM `user` WHERE username=@p0 AND `password`=@p1";
            var usernamePasswordCorretOrNot = UserData.QueryIfUserExist(sql,new MySqlParameter("p0",username),new MySqlParameter("p1",password));
            if (usernamePasswordCorretOrNot)
            {
                HttpContext.Session.SetString("username",username);
                
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
