using System.Threading.Tasks;
using CA_Proj.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return View(await _context.Products.ToListAsync());
        }
    }
}
