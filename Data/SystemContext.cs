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
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");
                
            });
            modelBuilder.Entity<ProductActivationCode>().ToTable("product_activation_code");
            modelBuilder.Entity<Purchase>().ToTable("purchase");
            modelBuilder.Entity<PurchaseProduct>(entity => 
            {
                entity.HasKey(vf => new { vf.PurchaseId, vf.ProductId });//if you don't use composite key here, the same purchase will only have one order.
                entity.ToTable("purchase_product");
                //entity.HasOne(p => p.Product).WithMany(b => b.purchaseProducts).HasForeignKey(p => p.ProductId);
            });
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductActivationCode> ProductActivationCodes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseProduct> PurchaseProducts { get; set; }
    }  
}