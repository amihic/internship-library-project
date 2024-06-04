using LibraryProject.Domain.Interfaces;
using LibraryProject.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Data.Repositories
{
    public class RentBookRepository : IRentBookRepository
    {
        private readonly LibraryDbContext _libraryDbContext;

        public RentBookRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<RentBook> AddAsync(RentBook rentBook)
        {
            _libraryDbContext.Rents.Add(rentBook);
            SaveChanges();

            return rentBook;
        }

        public async Task<RentBook> GetRentByUserIdAsync(string userId)
        {
            var rent = await _libraryDbContext.Rents
                                .Include(rb => rb.User)
                                .Include(rb => rb.Book)
                                .FirstOrDefaultAsync(rent => rent.User.Id == userId && rent.DateReturned == null);

            return rent;
        }

        public async Task<RentBook> GetRentByUserIdAndBookIdAsync(string userId, int bookId)
        {
            var rent = await _libraryDbContext.Rents
                                .Include(rb => rb.User)
                                .Include(rb => rb.Book)
                                .FirstOrDefaultAsync(rent => rent.User.Id == userId && rent.Book.Id == bookId && rent.DateReturned == null);

            return rent;
        }

        public async Task<List<RentBook>> GetRentsByBookIdAsync(int bookId)
        {
            var bookRents = await _libraryDbContext.Rents
                                .Include(rb => rb.User)
                                .Include(rb => rb.Book)
                                .Where(rent => rent.BookId == bookId && rent.DateReturned == null)
                                .ToListAsync();
            return bookRents;
        }

        public async Task<List<RentBook>> GetAllRentsByUserIdAsync(string userId)
        {
            var rents = await _libraryDbContext.Rents
                                .Include(rb => rb.User)
                                .Include(rb => rb.Book)
                                .Where(rent => rent.User.Id == userId)
                                .ToListAsync();

            return rents;
        }

        public async Task<List<RentBook>> GetAllRentsByBookIdAsync(int bookId)
        {
            var bookRents = await _libraryDbContext.Rents
                                .Include(rb => rb.User)
                                .Include(rb => rb.Book)
                                .Where(rent => rent.BookId == bookId)
                                .ToListAsync();
            return bookRents;
        }

        public async Task<RentBook> UpdateBookAsync(RentBook rentBook)
        {
            _libraryDbContext.Rents.Update(rentBook);
            SaveChanges();

            return rentBook;
        }

        public void SaveChanges()
        {
            _libraryDbContext.SaveChanges();
        }
    }
}
