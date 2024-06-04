using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Helpers;
using LibraryProject.Domain.Interfaces;
using LibraryProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryProject.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<Result<BookDTO>> CreateBookAsync(CreateBookDTO createBookDTO)
        {
            var authors = await _authorRepository.GetAuthorsByIdsAsync(createBookDTO.AuthorIds);

            if (authors == null || !authors.Any())
            {
                return Result<BookDTO>.Failure("At least one author is invalid.");
            }

            var newBook = new Book
            {
                Authors = authors.ToList(),
                Title = createBookDTO.Title,
                Isbn = createBookDTO.Isbn,
                Genre = createBookDTO.Genre,
                NumberOfPages = createBookDTO.NumberOfPages,
                PublishingYear = createBookDTO.PublishingYear,
                TotalCopies = createBookDTO.TotalCopies,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _bookRepository.CreateBookAsync(newBook);

            var bookDTO = await MapToBookDTO(newBook);
            return Result<BookDTO>.Success(bookDTO);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<Result<BookDTO>> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);

            if (book == null)
            {
                return Result<BookDTO>.Failure("Book does not exist!");
            }

            var bookToReturn = await MapToBookDTO(book);
            return Result<BookDTO>.Success(bookToReturn);
        }

        public async Task<List<BookDTO>> GetBooksAsync()
        {
            var books = await _bookRepository.GetBooksAsync();

            var booksToReturn = new List<BookDTO>();

            foreach (var book in books)
            {
                var bookDTO = await MapToBookDTO(book);
                booksToReturn.Add(bookDTO);
            }

            return booksToReturn;
        }

        public async Task<Result<BookDTO>> UpdateBookAsync(UpdateBookDTO updateBookDTO)
        {
            var bookToUpdate = await _bookRepository.GetBookByIdAsync(updateBookDTO.Id);

            if (bookToUpdate == null)
            {
                return Result<BookDTO>.Failure("Book not found.");
            }

            var authors = await _authorRepository.GetAuthorsByIdsAsync(updateBookDTO.AuthorIds);

            if (authors == null || !authors.Any())
            {
                return Result<BookDTO>.Failure("At least one author is invalid.");
            }

            bookToUpdate.Authors = authors.ToList();
            bookToUpdate.Title = updateBookDTO.Title;
            bookToUpdate.Isbn = updateBookDTO.Isbn;
            bookToUpdate.Genre = updateBookDTO.Genre;
            bookToUpdate.NumberOfPages = updateBookDTO.NumberOfPages;
            bookToUpdate.PublishingYear = updateBookDTO.PublishingYear;
            bookToUpdate.TotalCopies = updateBookDTO.TotalCopies;
            bookToUpdate.LastUpdatedAt = DateTime.UtcNow;

            await _bookRepository.UpdateBookAsync(bookToUpdate);

            var bookDTO = await MapToBookDTO(bookToUpdate);
            return Result<BookDTO>.Success(bookDTO);
        }

        private async Task<BookDTO> MapToBookDTO(Book book)
        {
            var bookToReturn = new BookDTO
            {
                Authors = book.Authors.Select(author => new AuthorDTO
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName
                }).ToList(),
                Title = book.Title,
                Isbn = book.Isbn,
                Genre = book.Genre,
                NumberOfPages = book.NumberOfPages,
                PublishingYear = book.PublishingYear,
                TotalCopies = book.TotalCopies,
            };

            return bookToReturn;
        }
    }
}
