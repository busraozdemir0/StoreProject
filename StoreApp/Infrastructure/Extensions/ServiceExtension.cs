using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Infrastructure.Extensions
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
                options.EnableSensitiveDataLogging(true);
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser,IdentityRole>(options=>
            {
                options.SignIn.RequireConfirmedAccount=false;
                options.User.RequireUniqueEmail=true; // aynı emailden bir kere daha girilemesin
                options.Password.RequireLowercase=false; // şifrede küçük harf zorunluluğu olmasın
                options.Password.RequireUppercase=false; // şifrede büyük harf zorunluluğu olmasın
                options.Password.RequireDigit=false; // şifre de rakam zorunluluğu olmasın
                options.Password.RequiredLength=6;
            })
            .AddEntityFrameworkStores<RepositoryContext>();
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
            services.AddScoped<IAuthService, AuthManager>();
        }

        // url yönlendirmesinde ilgili yazılar veya yönlendirmeler küçük harf olması için (../product/get/2 gibi)
        public static void ConfigureRouting(this IServiceCollection services)
        {
            services.AddRouting(options => {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false; // en sona bir slash(/) eklenmemesi için
            });
        }
    }
}
