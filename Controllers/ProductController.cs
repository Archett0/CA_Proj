using System.Threading.Tasks;
using CA_Proj.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CA_Proj.Controllers
{
    public class ProductController : Controller
    {
        private readonly SystemContext _context;

        public ProductController(SystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetObject<string>("userid")))
            {
                HttpContext.Session.SetObject("userid", 1);
                HttpContext.Session.SetObject("cart_upto_date", false);
            }
            return View(await _context.Products.ToListAsync());
        }
    }
}
