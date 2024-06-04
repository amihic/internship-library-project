using Domain.Interfaces;
using LibraryProject.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryProject.API.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("change-password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = await _userService.ChangePasswordAsync(userEmail, changePasswordDTO);

            if (!result)
            {
                return BadRequest("Failed to change password. Please check your current password.");
            }

            return Ok("Password changed successfully.");
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO updateProfileDTO)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = await _userService.UpdateProfileAsync(userEmail, updateProfileDTO);

            if (!result)
            {
                return BadRequest("Failed to update profile.");
            }

            return Ok("User profile updated successfully.");
        }
    }
}
