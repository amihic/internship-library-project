using LibraryProject.Domain.Constants;
using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Helpers;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Domain.Model;
using LibraryProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Authorize(Roles = LibraryRoles.Librarian)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO createBookDTO)
        {
            var newBook = await _bookService.CreateBookAsync(createBookDTO);

            if (!newBook.IsSuccess)
            {
                return BadRequest(new { message = newBook.ErrorMessage });
            }

            return Ok(new
            {
                message = "Book is successfully created!",
                book = newBook
            });
        }

        [HttpPut]
        [Authorize(Roles = LibraryRoles.Librarian)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDTO updateBookDTO)
        {
            var bookToUpdate = await _bookService.UpdateBookAsync(updateBookDTO);

            if (!bookToUpdate.IsSuccess)
            {
                return BadRequest(new { message = bookToUpdate.ErrorMessage });
            }

            return Ok(new
            {
                message = "Book is successfully updated!",
                book = bookToUpdate.Data
            });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooksAsync();

            return Ok(books);
        }

        [HttpGet("{Id:int}")]
        [Authorize(Roles = LibraryRoles.Librarian)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookById(int Id)
        {
            var book = await _bookService.GetBookByIdAsync(Id);

            if (!book.IsSuccess)
            {
                return BadRequest(new { message = book.ErrorMessage });
            }

            return Ok(book);
        }

        [HttpDelete("{Id:int}")]
        [Authorize(Roles = LibraryRoles.Librarian)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookDeleted = await _bookService.DeleteBookAsync(id);

            if (!bookDeleted)
            {
                return BadRequest(new { message = "Book does not exist!" });
            }

            return Ok("Book is successfully deleted!");
        }
    }
}
