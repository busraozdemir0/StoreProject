using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructe.Extensions
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
    }
}