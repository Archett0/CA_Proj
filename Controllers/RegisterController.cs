using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using CA_Proj.Data;
using MySql.Data.MySqlClient;
using System.Linq;

namespace CA_Proj.Controllers
{
	public class RegisterController : Controller
	{
        private SystemContext _context;

        public RegisterController(SystemContext context)
        {
            _context = context;
        }
        public IActionResult Register(IFormCollection form)
        {
            string username = form["username"];
            if(username == "default_customer")
            {
                return View();
            }
            string password = form["password"];
            var query = _context.Users.AsQueryable();
            var result = query.Where(x => x.Username == username && x.Password == password);
            if (result.Count() >= 1)
            {
                return Json(new
                {
                    success = false,
                    message= "The user name already exists!"
                });
            }
            else
            {
                _context.Users.Add(new Models.User(_context.Users.Count()+1,username,password,username));
                _context.SaveChanges();
            }
            //id(The "id" column will be set as the primary key and will be auto-incrementing.),username,password,nickname
            return RedirectToAction("Index", "Product");
        }
    }
}
