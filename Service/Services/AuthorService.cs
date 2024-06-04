using Domain.Interfaces;
using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Domain.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> CreateAuthorAsync(CreateAuthorDTO createAuthorDTO)
        {
            var newAuthor = new Author
            {
                FirstName = createAuthorDTO.FirstName,
                LastName = createAuthorDTO.LastName,
                YearOfBirth = createAuthorDTO.YearOfBirth,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            return await _authorRepository.CreateAuthorAsync(newAuthor);
        }

        public async Task<bool> DeleteAuthorAsync(Guid id)
        {
            return await _authorRepository.DeleteAuthorAsync(id);
        }

        public async Task<Author> GetAuthorByIdAsync(Guid id)
        {
            return await _authorRepository.GetAuthorByIdAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _authorRepository.GetAuthorsAsync();
        }

        public async Task<Author> UpdateAuthorAsync(Guid id, UpdateAuthorDTO updateAuthorDTO)
        {
            var authorToUpdate = await _authorRepository.GetAuthorByIdAsync(id);

            if (authorToUpdate == null)
            {
                return null;
            }
            
            authorToUpdate.FirstName = updateAuthorDTO.FirstName;
            authorToUpdate.LastName = updateAuthorDTO.LastName;
            authorToUpdate.YearOfBirth = updateAuthorDTO.YearOfBirth;
            authorToUpdate.LastUpdatedAt = DateTime.UtcNow;

            return await _authorRepository.UpdateAuthorAsync(authorToUpdate);
        }
    }
}
