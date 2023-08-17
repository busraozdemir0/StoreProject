using StoreApp.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Controller ve View'ların birlikte kullanılacağını ifade ettik

builder.Services.AddRazorPages(); // RazorPage sayfaları controller olmadan da sayfaları yapmamıza yardımcı olur

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity(); // identity kütüphanesi için yazdığımız eklenti metodunun servis kaydı

builder.Services.ConfigureSession(); //Session yönetimini configüre haline getirerek metot yaptık. Burda da metodu çağırıyoruz.

builder.Services.ConfigureRepositoryRegistration();

builder.Services.ConfigureServiceRegistration();

builder.Services.ConfigureRouting(); // url yönlendirmesinde ilgili yazılar veya yönlendirmeler küçük harf olması için(../product/get/2 gibi)


builder.Services.AddAutoMapper(typeof(Program));  // AutoMapper => Dto tanımlarını otomatik olarak nesnelere dönüştüren servis kaydı


var app = builder.Build();

app.UseStaticFiles();   // Static dosyalara ulaşabilmek için
app.UseSession();  // Oturumları kullanabilmek için

app.UseHttpsRedirection();
app.UseRouting();

//endpointlerin altında yazarsak bazı kodlarımız çalışmayabiliyor(Bu yüzden routing ve endpointler arasında yazdık)
app.UseAuthentication();  // oturum açma
app.UseAuthorization();  // yetkilendirme

app.ConfigureLocalization(); // Localization metodu

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



app.ConfigureAndCheckMigration(); // metot sayesinde migration'u update etmemize gerek kalmayacak çünkü bu metot otomatik migrate işlemini yapacak

app.ConfigureDefaultAdminUser(); // admin rolünde varsayılan bir kullanıcı oluşturan eklenti metot

app.Run();
