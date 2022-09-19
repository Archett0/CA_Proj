using CA_Proj.Models;
using Microsoft.EntityFrameworkCore;

namespace CA_Proj.Data
{
    public class SystemContext: DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}