using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles{get;} // sistemdeki tüm rolleri getirmek için
        IEnumerable<IdentityUser> GetAllUsers(); // sistemdeki tüm kullanıcıları getirmek için
        Task<IdentityResult> CreateUser(UserDtoForCreation userDto);
    }
}