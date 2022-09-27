using CA_Proj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace CA_Proj.Controllers
{ 
    [Route("_NavBar")]
    public class LoginController : Controller
    {
        private UserDB db;
        public LoginController(IConfiguration cfg)
        {
            db = new UserDB(cfg.GetConnectionString("SystemContext"));
        }
        public IActionResult Login(IFormCollection form)
        {
            // data from client
            string username = form["username"];
            string password = form["password"];

            User user = db.GetUserByUsername(username);
            if (user != null)
            {
                // check if provided password matches the one in the database
                if (user.Password == password)
                {
                    // add a new session for this user who has login successfully
                    string sessionId = db.AddSession(user.Id);

                    // ask browser to save these cookies and send back next time
                    Response.Cookies.Append("SessionId", sessionId);

                    // send user to Product page
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Product");
        }

    }
}
