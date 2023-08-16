using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ApplicationExtension // static sınıfların bütün üyeleri static olur ve static sınıfları new'leyemeyiz.
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        {
            RepositoryContext context=app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RepositoryContext>();       
            
            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();  //uygulama kendisi bakacak ve eğer migrate işlemi varsa otomatik olarak migration işlemini yapacak 
            }

        }

        // Localization
        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options=>
            {
                options.AddSupportedCultures("tr-TR") // para sembolünün dolar değil tl olarak gelmesi için
                    .AddSupportedUICultures("tr-TR")
                    .SetDefaultCulture("TR-tr");
            });
        }

        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
        {
            const string adminUser="Admin";
            const string adminPassword="Admin+123456";

            // UserManager
            UserManager<IdentityUser> userManager=app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            // RoleManager
            RoleManager<IdentityRole> roleManager=app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user= await userManager.FindByNameAsync(adminUser);
            if(user is null)
            {
                user=new IdentityUser() // kullanıcı yoksa oluşturacak
                {
                    Email="busra@gmail.com",
                    PhoneNumber="5554443322",
                    UserName=adminUser,
                };

                var result=await userManager.CreateAsync(user,adminPassword);

                if(!result.Succeeded)
                {
                    throw new Exception("Admin user could not created.");
                }

                var roleResult=await userManager.AddToRolesAsync(user,
                    roleManager
                    .Roles
                    .Select(r=>r.Name)
                    .ToList()
                );

                if(!roleResult.Succeeded)
                {
                    throw new Exception("System have problems with role defination for admin.");
                }
            }
        }
    }
}