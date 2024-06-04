using Domain.Interfaces;
using LibraryProject.Data;
using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Security.Claims;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            
            return roles;
        }
        public async Task<User> GetUserByEmailAsync(string email) 
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            return user;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<User> GetCorrectUserAsync(LogInDTO logInDTO)
        {
            var user = await _userManager.FindByEmailAsync(logInDTO.Email);

            if (user == null)
            {
                return null;
            }

            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, logInDTO.Password);
            if (!isCorrectPassword)
            {
                return null;
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> RegisterAsync(User user, string role)
        {
            await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, role);

            return user;
        }
    }
}
