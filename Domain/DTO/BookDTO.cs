﻿using LibraryProject.Domain.Constants;
using LibraryProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Domain.DTO
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public ICollection<AuthorDTO> Authors { get; set; }
        public BookGenres Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int PublishingYear { get; set; }
        public int TotalCopies { get; set; }
    }
}
