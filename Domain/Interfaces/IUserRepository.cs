using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> RegisterAsync(User user, string role);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(string id);
        Task<bool> UpdateAsync(User user);
        Task<User> GetCorrectUserAsync(LogInDTO logInDTO);
        Task<IList<string>> GetUserRolesAsync(User user);
    }
}
