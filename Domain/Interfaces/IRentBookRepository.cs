using LibraryProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Domain.Interfaces
{
    public interface IRentBookRepository
    {
        Task<RentBook> AddAsync(RentBook rentBook);
        Task<List<RentBook>> GetRentsByBookIdAsync(int bookId);
        Task<List<RentBook>> GetAllRentsByBookIdAsync(int bookId);
        Task<RentBook> GetRentByUserIdAsync(string userId);
        Task<List<RentBook>> GetAllRentsByUserIdAsync(string userId);
        Task<RentBook> GetRentByUserIdAndBookIdAsync(string userId, int bookId);
        Task<RentBook> UpdateBookAsync(RentBook rentBook);
        void SaveChanges();
    }
}
