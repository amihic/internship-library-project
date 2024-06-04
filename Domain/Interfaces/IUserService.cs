using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> RegisterAsync(RegisterDTO registerDTO, string role);
        Task<string> GetUsersTokenAsync(LogInDTO logInDTO);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ChangePasswordAsync(string email, ChangePasswordDTO changePasswordDTO);
        Task<bool> UpdateProfileAsync(string email, UpdateProfileDTO updateProfileDTO);
    }
}
