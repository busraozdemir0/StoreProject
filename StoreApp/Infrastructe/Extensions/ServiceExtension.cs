using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Infrastructe.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<RepositoryContext>(options =>
            {
                options.UseSqlite(
                    configuration.GetConnectionString("sqlconnection"),
                    b => b.MigrationsAssembly("StoreApp")
                ); //Migrations ifadeleri StoreApp klasörü içerisinde oluşacak
            });
        }

        public static void ConfigureSession(this IServiceCollection services)
        {
            // Oturum yönetimi için iki tane servis
            services.AddDistributedMemoryCache(); // sunucu tarafındaki bilgileri tutar
            services.AddSession(options =>
            {
                options.Cookie.Name = "StoreApp.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(180); // 180 dk sonra oturumdan düşecek
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           
            services.AddScoped<Cart>(c=>SessionCart.GetCart(c)); // Bu servis kaydı ile her kullanıcı ayrı bir cart nesnesi kullanacak

        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>(); // ilgili ifadenin tanımlanmasını gerçekleştirdik
            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IOrderService, OrderManager>();
        }
    }
}
