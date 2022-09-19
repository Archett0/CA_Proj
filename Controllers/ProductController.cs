using Microsoft.AspNetCore.Mvc;

namespace CA_Proj.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
