using Domain.Interfaces;
using LibraryProject.Domain.Constants;
using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Domain.Model;
using LibraryProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.API.Controllers
{
    [ApiController]
    [Authorize(Roles = LibraryRoles.Librarian)]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AuthorDTO>>> GetAuthors()
        {
            //var authors = await _authorService.GetAuthorsAsync();
            var authors = new List<AuthorDTO>();

            var author1 = new AuthorDTO
            {
                FirstName = "Ivo",
                LastName = "Andric"
            };

            var author2 = new AuthorDTO
            {
                FirstName = "Leo",
                LastName = "Tolstoy"
            };

            var author3 = new AuthorDTO
            {
                FirstName = "William",
                LastName = "Shakespeare"
            };

            var author4 = new AuthorDTO
            {
                FirstName = "Charles",
                LastName = "Dickens"
            };

            var author5 = new AuthorDTO
            {
                FirstName = "Ernest",
                LastName = "Hemingway"
            };

            var author6 = new AuthorDTO
            {
                FirstName = "Agatha",
                LastName = "Christie"
            };

            authors.Add(author1);
            authors.Add(author2);
            authors.Add(author3);
            authors.Add(author4);
            authors.Add(author5);
            authors.Add(author6);

            return Ok(authors);
        }

        [HttpGet("{Id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Author>> GetAuthorById(Guid id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return BadRequest("Author with that Id does not exist!");
            }

            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Author>> CreateAuthor([FromBody] CreateAuthorDTO createAuthorDTO)
        {
            var newAuthor = await _authorService.CreateAuthorAsync(createAuthorDTO);

            if (newAuthor == null)
            {
                return BadRequest("Author already exists!");
            }

            return Ok("Author is successfully created!");
        }

        [HttpPut("{Id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Author>> UpdateAuthor(Guid id, [FromBody] UpdateAuthorDTO updateAuthorDTO)
        {
            var authorToUpdate = await _authorService.UpdateAuthorAsync(id, updateAuthorDTO);

            if (authorToUpdate == null)
            {
                return BadRequest("Author does not exist!");
            }

            return Ok("Author is successfully updated!");
        }

        [HttpDelete("{Id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Author>> DeleteAuthor(Guid id)
        {
            var authorDeleted = await _authorService.DeleteAuthorAsync(id);

            if (!authorDeleted)
            {
                return BadRequest("Author does not exist!");
            }

            return Ok("Author is successfully deleted!");
        }
    }
}
