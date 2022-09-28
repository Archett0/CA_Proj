using CA_Proj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using System;

namespace CA_Proj.Controllers
{
	public class RegisterController : Controller
	{
        /*private UserDB db;
        public RegisterController(IConfiguration cfg)
        {
            db = new UserDB(cfg.GetConnectionString("SystemContext"));
        }
        public IActionResult Register(IFormCollection form)
        {
            // data from client
            string username = form["username"];
            string password = form["password"];

            User user = db.GetUserByUsername(username);
            if (user != null)
            {
                Console.WriteLine("11111111");
                // check if provided password matches the one in the database
                if (user.Password == password)
                {
                    // add a new session for this user who has login successfully
                    string sessionId = db.AddSession(user.Id);

                    // ask browser to save these cookies and send back next time
                    Response.Cookies.Append("SessionId", sessionId);

                    return RedirectToAction("Index", "Product");
                }
            }
            return RedirectToAction("Index", "Product");
        }*/
    }
}
