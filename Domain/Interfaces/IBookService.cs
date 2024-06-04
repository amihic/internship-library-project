using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Helpers;
using LibraryProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Domain.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetBooksAsync();
        Task<Result<BookDTO>> GetBookByIdAsync(int id);
        Task<bool> DeleteBookAsync(int id);
        Task<Result<BookDTO>> CreateBookAsync(CreateBookDTO createBookDTO);
        Task<Result<BookDTO>> UpdateBookAsync(UpdateBookDTO updateBookDTO);
    }
}
