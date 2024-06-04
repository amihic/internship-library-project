using LibraryProject.Domain.DTO;
using LibraryProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Domain.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(Guid id);
        Task<Author> CreateAuthorAsync(CreateAuthorDTO createAuthorDTO);
        Task<Author> UpdateAuthorAsync(Guid id, UpdateAuthorDTO updateAuthorDTO);
        Task<bool> DeleteAuthorAsync(Guid id);
    }
}
