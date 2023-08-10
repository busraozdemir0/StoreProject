using Microsoft.EntityFrameworkCore;
using StoreApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Controller ve View'ların birlikte kullanılacağını ifade ettik
builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"));
});

var app = builder.Build();

app.UseStaticFiles();   // Static dosyalara ulaşabilmek için
app.UseHttpsRedirection();
app.UseRouting();

app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}/{id?}");

app.Run();
