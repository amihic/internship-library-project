using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public AuthorRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            _libraryDbContext.Authors.Add(author);
            SaveChanges();

            return author;
        }

        public async Task<bool> DeleteAuthorAsync(Guid id)
        {
            var author = await GetAuthorByIdAsync(id);
            if (author == null) 
            {
                return false;    
            }

            author.IsDeleted = true;
            await UpdateAuthorAsync(author);

            return true;
        }

        public async Task<Author> GetAuthorByIdAsync(Guid id)
        {
            var author = await _libraryDbContext.Authors
                .Where(c => c.Id == id && c.IsDeleted == false)
                .FirstOrDefaultAsync();

            return author;
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _libraryDbContext.Authors
                .Where(author => author.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsByIdsAsync(List<Guid> authorIds)
        {
            if (authorIds == null || !authorIds.Any())
            {
                return Enumerable.Empty<Author>();
            }

            var authors = await _libraryDbContext.Authors
                .Where(author => authorIds.Contains(author.Id) && !author.IsDeleted)
                .ToListAsync();
           
            if (authors.Count != authorIds.Count)
            {
                return Enumerable.Empty<Author>();
            }

            return authors;
        }
        public void SaveChanges()
        {
            _libraryDbContext.SaveChanges();
        }

        public async Task<Author> UpdateAuthorAsync(Author author)
        {
             _libraryDbContext.Authors.Update(author);
            SaveChanges();

            return author;
        }
    }
}
