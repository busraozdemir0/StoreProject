using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Controller ve View'ların birlikte kullanılacağını ifade ettik
builder.Services.AddRazorPages(); // RazorPage sayfaları controller olmadan da sayfaları yapmamıza yardımcı olur

builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"),
    b=>b.MigrationsAssembly("StoreApp")); //Migrations ifadeleri StoreApp klasörü içerisinde oluşacak
});
// Oturum yönetimi için iki tane servis
builder.Services.AddDistributedMemoryCache(); // sunucu tarafındaki bilgileri tutar
builder.Services.AddSession(options=>
{
    options.Cookie.Name="StoreApp.Session";
    options.IdleTimeout=TimeSpan.FromMinutes(180); // 180 dk sonra oturumdan düşecek
});
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

builder.Services.AddScoped<IRepositoryManager,RepositoryManager>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>(); // ilgili ifadenin tanımlanmasını gerçekleştirdik

builder.Services.AddScoped<IServiceManager,ServiceManager>(); 
builder.Services.AddScoped<IProductService,ProductManager>(); 
builder.Services.AddScoped<ICategoryService,CategoryManager>(); 

builder.Services.AddSingleton<Cart>(); // Bu servis kaydı ile herkes aynı sepeti kullanıyor(ilerde oturum yönetimi ile kişiye özel olacak)

builder.Services.AddAutoMapper(typeof(Program));  // AutoMapper => Dto tanımlarını otomatik olarak nesnelere dönüştüren servis kaydı


var app = builder.Build();

app.UseStaticFiles();   // Static dosyalara ulaşabilmek için
app.UseSession();  // Oturumları kullanabilmek için

app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints=> {
    endpoints.MapAreaControllerRoute(   //Area için endpoint tanımı yaptık
        name:"Admin",
        areaName:"Admin",
        pattern:"Admin/{controller=Dashboard}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name:"default",
        pattern:"{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapRazorPages();
});
   

app.Run();
