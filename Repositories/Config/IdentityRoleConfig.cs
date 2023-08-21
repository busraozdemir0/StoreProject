using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData( // veritabanı sıfırlansa bile en başta bu veriler otomatik kaydedilmiş olacaktır
                new IdentityRole(){ Name="User", NormalizedName="USER" },
                new IdentityRole(){ Name="Editor", NormalizedName="EDITOR" },
                new IdentityRole(){ Name="Admin", NormalizedName="ADMIN" }
            );
        }
    }
}