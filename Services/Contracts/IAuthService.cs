using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles{get;} // sistemdeki tüm rolleri getirmek için
        IEnumerable<IdentityUser> GetAllUsers(); // sistemdeki tüm kullanıcıları getirmek için
        Task<IdentityUser> GetOneUser(string userName);
        Task<UserDtoForUpdate> GetOneUserForUpdate(string userName);
        Task<IdentityResult> CreateUser(UserDtoForCreation userDto);
        Task Update (UserDtoForUpdate userDto);
        Task<IdentityResult> ResetPassword(ResetPasswordDto model);
        Task<IdentityResult> DeleteOneUser(string userName);
    }
}