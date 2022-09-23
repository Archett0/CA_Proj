using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CA_Proj.Data;
using Microsoft.EntityFrameworkCore;

namespace CA_Proj.Controllers
{
    public class CartController : Controller
    {
        private SystemContext _context;

        public CartController(SystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ret =_context.PurchaseProducts.Include(p => p.Product).ToListAsync();
            return View(await ret);
        }
    }
}
