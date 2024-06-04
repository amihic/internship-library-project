using Domain.Interfaces;
using LibraryProject.Domain.Constants;
using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;


namespace LibraryProject.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = LibraryRoles.Librarian)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpPost("register-librarian")]
        [Authorize(Roles = LibraryRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> RegisterLibrarian([FromBody] RegisterDTO registerDTO)
        {
            var role = LibraryRoles.Librarian;

            var newUser = await _userService.RegisterAsync(registerDTO, role);

            if (newUser == null)
            {
                return BadRequest("User already exists!");
            }
            return Ok("Librarian is successfully created!");
        }

        [HttpPost("register-user")]
        [Authorize(Roles = LibraryRoles.Librarian)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> RegisterUser([FromBody] RegisterDTO registerDTO)
        {
            var role = LibraryRoles.User;

            var newUser = await _userService.RegisterAsync(registerDTO, role);

            if (newUser == null)
            {
                return BadRequest("User already exists!");
            }
            return Ok("User is successfully created!");
        }

        [HttpPost("login")]
        [SwaggerOperation(Description = "User login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LogInDTO logInDTO)
        {
            var token = await _userService.GetUsersTokenAsync(logInDTO);

            if (token == null)
            {
                return BadRequest("Invalid email or password!");
            }
            return Ok(new { token });
        }
    }
}
