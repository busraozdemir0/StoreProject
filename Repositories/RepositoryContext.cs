﻿using System.Reflection;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Repositories.Config;
using SQLitePCL;
using Microsoft.AspNetCore.Identity;

namespace Repositories
{
    public class RepositoryContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
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