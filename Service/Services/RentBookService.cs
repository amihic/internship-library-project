using Domain.Interfaces;
using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Helpers;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Service.Services
{
    public class RentBookService : IRentBookService
    {
        private readonly IRentBookRepository _rentBookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;

        public RentBookService(IRentBookRepository rentBookRepository, IUserRepository userRepository, IBookRepository bookRepository)
        {
            _rentBookRepository = rentBookRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public async Task<List<RentDetailsDTO>> GetAllRentsByBookIdAsync(int bookId)
        {
            var rents = await _rentBookRepository.GetAllRentsByBookIdAsync(bookId);

            var bookRents = rents.Select(rent => MapToRentBookDTO(rent)).ToList();

            return bookRents;
        }

        public async Task<List<RentDetailsDTO>> GetRentsByUserIdAsync(string userId)
        {
            var rents = await _rentBookRepository.GetAllRentsByUserIdAsync(userId);

            var userRents = rents.Select(rent => MapToRentBookDTO(rent)).ToList();

            return userRents;
        }

        public async Task<Result<RentDetailsDTO>> RentBookAsync(RentReturnBookDTO rentBookDTO)
        {
            Console.WriteLine("Book is rented...");

            var user = await _userRepository.GetUserByIdAsync(rentBookDTO.UserId);

            if (user == null)
            {
                return Result<RentDetailsDTO>.Failure("User does not exist!");
            }
            
            var book = await _bookRepository.GetBookByIdAsync(rentBookDTO.BookId);

            if (book == null)
            {
                return Result<RentDetailsDTO>.Failure("Book does not exist!");
            }

            var bookRents = await _rentBookRepository.GetRentsByBookIdAsync(book.Id);
            int numberOfBookRents = bookRents.Count;
            
            if (numberOfBookRents >= book.TotalCopies)
            {
                return Result<RentDetailsDTO>.Failure("All copies of this book have been rented!");
            }

            var rentBook = new RentBook
            {
                User = user,
                Book = book,
                DateRented = DateTime.UtcNow,
                DateReturned = null
            };

            await _rentBookRepository.AddAsync(rentBook);

            Console.WriteLine("Book is rented...");

            var rentToReturn = MapToRentBookDTO(rentBook);
            return Result<RentDetailsDTO>.Success(rentToReturn);
        }

        public async Task<Result<RentDetailsDTO>> ReturnBookAsync(RentReturnBookDTO returnRentBookDTO)
        {
            //Console.WriteLine("Returning book...");

            var returnBook = await _rentBookRepository.GetRentByUserIdAndBookIdAsync(returnRentBookDTO.UserId, returnRentBookDTO.BookId);

            if (returnBook == null)
            {
                return Result<RentDetailsDTO>.Failure("User or book is not valid");
            }
            
            returnBook.DateReturned = DateTime.UtcNow;

            await _rentBookRepository.UpdateBookAsync(returnBook);

            //Console.WriteLine("Book returned...");
            Console.WriteLine("Book returned...");

            var returnedBook = MapToRentBookDTO(returnBook);
            return Result<RentDetailsDTO>.Success(returnedBook);
        }

        private RentDetailsDTO MapToRentBookDTO(RentBook rentBook)
        {
            var rentBookToReturn = new RentDetailsDTO
            {
                User = new RentUserDetailsDTO
                {
                    FirstName = rentBook.User.FirstName,
                    LastName = rentBook.User.LastName
                },
                Book = new RentBookDetailsDTO
                {
                    Id = rentBook.Book.Id,
                    Title = rentBook.Book.Title
                },
                DateRented = rentBook.DateRented,
                DateReturned = rentBook.DateReturned
                
            };

            return rentBookToReturn;
        }
    }
}
