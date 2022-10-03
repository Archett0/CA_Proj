using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CA_Proj.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetString("userid", "0");
            return RedirectToAction("Index", "Product");
        }
    }
}
