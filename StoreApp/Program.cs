using StoreApp.Infrastructe.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Controller ve View'ların birlikte kullanılacağını ifade ettik

builder.Services.AddRazorPages(); // RazorPage sayfaları controller olmadan da sayfaları yapmamıza yardımcı olur

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.ConfigureSession(); //Session yönetimini configüre haline getirerek metot yaptık. Burda da metodu çağırıyoruz.

builder.Services.ConfigureRepositoryRegistration();

builder.Services.ConfigureServiceRegistration();


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
