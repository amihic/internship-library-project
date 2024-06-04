using LibraryProject.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Domain.DTO
{
    public class UpdateBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public List<Guid> AuthorIds { get; set; }
        public BookGenres Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int PublishingYear { get; set; }
        public int TotalCopies { get; set; }
    }
}
