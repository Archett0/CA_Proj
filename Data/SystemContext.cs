using CA_Proj.Models;
using Microsoft.EntityFrameworkCore;

namespace CA_Proj.Data
{
    public class SystemContext: DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("product");
            modelBuilder.Entity<User>().ToTable("user");
            // make Username to be a column that has unique values
            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}