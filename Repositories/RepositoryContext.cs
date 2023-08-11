using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) // base'si DbContext'dir.
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()  // Product tablosunda veri yoksa bu 5 kaydı tabloya ekleyecek
            .HasData(
                new Product() { ProductId = 1, ProductName = "Computer", Price = 17_000 },
                new Product() { ProductId = 2, ProductName = "Keyboard", Price = 1_000 },
                new Product() { ProductId = 3, ProductName = "Mouse", Price = 500 },
                new Product() { ProductId = 4, ProductName = "Monitor", Price = 7_000 },
                new Product() { ProductId = 5, ProductName = "Deck", Price = 1_500 }
            );
            // dotnet ef migrations add ProductSeedData => yukarıdaki kayıtları eklemek için
        }   // dotnet ef database update => değişikliklerin veritabanına yansıması için

    }
}