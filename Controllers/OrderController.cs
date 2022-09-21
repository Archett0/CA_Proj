using Microsoft.AspNetCore.Mvc;

namespace CA_Proj.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
