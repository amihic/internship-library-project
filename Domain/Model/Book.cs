using LibraryProject.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Domain.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public ICollection<Author> Authors { get; set; }
        public BookGenres Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int PublishingYear { get; set; }
        public int TotalCopies { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set;}
        public bool IsDeleted { get; set; }
        public ICollection<RentBook> Rents { get; set; }
    }
}
