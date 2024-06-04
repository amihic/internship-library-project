using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Domain.DTO
{
    public class RentDetailsDTO
    {
        public RentUserDetailsDTO User { get; set; }
        public RentBookDetailsDTO Book { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}
