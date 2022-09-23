using CA_Proj.Models;
using Microsoft.EntityFrameworkCore;

namespace CA_Proj.Data
{
    public class SystemContext: DbContext
    {
        private int count = 0;
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("product");

        }
        public DbSet<Product> Products { get; set; }
    }
}