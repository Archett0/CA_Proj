using System;
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

    
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 9)
        {
            ViewData["PageNow"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            return View(await _context.Products.ToListAsync());
        }
    }
}
