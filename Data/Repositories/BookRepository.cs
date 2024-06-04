using LibraryProject.Domain.Interfaces;
using LibraryProject.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public BookRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            _libraryDbContext.Books.Add(book);
            SaveChanges();

            return book;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book == null)
            {
                return false;
            }

            book.IsDeleted = true;
            await UpdateBookAsync(book);

            return true;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _libraryDbContext.Books
                .Include(c => c.Authors)
                .Where(c => c.Id == id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            return book;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _libraryDbContext.Books
                .Include(c => c.Authors)
                .Where(book => !book.IsDeleted)
                .ToListAsync();
        }

        public void SaveChanges()
        {
            _libraryDbContext.SaveChanges();
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            _libraryDbContext.Books.Update(book);
            SaveChanges();

            return book;
        }
    }
}
