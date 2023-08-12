using System.Reflection;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Config;
using SQLitePCL;

namespace Repositories
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) // base'si DbContext'dir.
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    // dotnet ef migrations add ProductSeedData => yukarıdaki kayıtları eklemek için
            // dotnet ef database update => değişikliklerin veritabanına yansıması için 

            base.OnModelCreating(modelBuilder);
            
            // modelBuilder.ApplyConfiguration(new ProductConfig());
            // modelBuilder.ApplyConfiguration(new CategoryConfig());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
           
       
        }   


    }
}