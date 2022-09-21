using Microsoft.AspNetCore.Mvc;

namespace CA_Proj.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
