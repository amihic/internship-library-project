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
    public interface IRentBookService
    {
        Task<Result<RentDetailsDTO>> RentBookAsync(RentReturnBookDTO rentBookDTO);
        Task<Result<RentDetailsDTO>> ReturnBookAsync(RentReturnBookDTO rentBookDTO);
        Task<List<RentDetailsDTO>> GetRentsByUserIdAsync(string userId);
        Task<List<RentDetailsDTO>> GetAllRentsByBookIdAsync(int bookId);

    }
}
