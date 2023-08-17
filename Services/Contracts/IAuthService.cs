using Microsoft.AspNetCore.Identity;

namespace Services.Contracts{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles{get;} // sistemdeki tüm rolleri getirmek için
        // IEnumerable<IdentityUser> GetAllUsers{get;}
    }
}