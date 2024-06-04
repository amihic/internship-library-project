using LibraryProject.Domain.Constants;
using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace LibraryProject.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class RentBookController : ControllerBase
    {
        private readonly IRentBookService _rentService;

        public RentBookController(IRentBookService rentService)
        {
            _rentService = rentService;
        }

        [HttpPost("rent-book")]
        [Authorize(Roles = LibraryRoles.Librarian)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RentBook([FromBody] RentReturnBookDTO rentBookDTO)
        {
            var rentBook = await _rentService.RentBookAsync(rentBookDTO);

            if (!rentBook.IsSuccess)
            {
                return BadRequest(new { message = rentBook.ErrorMessage });
            }

            return Ok(new
            {
                message = "The book is successfully rented!",
                book = rentBook
            });
        }

        [HttpPost("return-book")]
        [Authorize(Roles = LibraryRoles.Librarian)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReturnBook([FromBody] RentReturnBookDTO rentBookDTO)
        {
            var returnBook = await _rentService.ReturnBookAsync(rentBookDTO);

            if (!returnBook.IsSuccess)
            {
                return BadRequest(new { message = returnBook.ErrorMessage });
            }

            return Ok(new
            {
                message = "The book is successfully returned!",
                book = returnBook
            });
        }

        [HttpGet("my-rent-history")]
        [Authorize(Roles = LibraryRoles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRentsByUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userRents = await _rentService.GetRentsByUserIdAsync(userId);

            return Ok(userRents);
        }

        [HttpGet("users/{userId}/history")]
        [Authorize(Roles = LibraryRoles.Librarian)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRentsByAnyUserId(string userId)
        {
            var userRents = await _rentService.GetRentsByUserIdAsync(userId);

            return Ok(userRents);
        }

        [HttpGet("books/{bookId:int}/history")]
        [Authorize(Roles = LibraryRoles.Librarian + ", " + LibraryRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRentsByBookId(int bookId)
        {
            var userRents = await _rentService.GetAllRentsByBookIdAsync(bookId);

            return Ok(userRents);
        }
    }
}
