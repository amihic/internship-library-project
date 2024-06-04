using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using LibraryProject.Domain.Model;
using LibraryProject.Domain.DTO;

namespace LibraryProject.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            return user;
        }

        public async Task<bool> ChangePasswordAsync(string email, ChangePasswordDTO changePasswordDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            var passwordHasher = new PasswordHasher<User>();

            if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, changePasswordDTO.CurrentPassword) != PasswordVerificationResult.Success)
            {
                return false;
            }

            var newPasswordHash = passwordHasher.HashPassword(user, changePasswordDTO.NewPassword);

            user.PasswordHash = newPasswordHash;

            return await _userRepository.UpdateAsync(user);
        }



        public async Task<string> GetUsersTokenAsync(LogInDTO logInDTO)
        {
            var userToLogIn = await _userRepository.GetCorrectUserAsync(logInDTO);
            if (userToLogIn == null)
            {
                return null;
            }

            var roles = await GetUserRolesAsync(userToLogIn);

            var token = await GenerateJwtToken(userToLogIn, roles);

            return token;
        }

        private async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _userRepository.GetUserRolesAsync(user);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task<User> RegisterAsync(RegisterDTO registerDTO, string role)
        {
            var user = await _userRepository.GetUserByEmailAsync(registerDTO.Email);
            if (user != null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher<User>();

            var newUser = new User
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName
            };

            newUser.PasswordHash = passwordHasher.HashPassword(newUser, registerDTO.Password);

            return await _userRepository.RegisterAsync(newUser, role);
        }

        private async Task<string> GenerateJwtToken(User user, IList<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
                };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> UpdateProfileAsync(string email, UpdateProfileDTO updateProfileDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            user.FirstName = updateProfileDTO.FirstName;
            user.LastName = updateProfileDTO.LastName;

            return await _userRepository.UpdateAsync(user);
        }
    }
}
