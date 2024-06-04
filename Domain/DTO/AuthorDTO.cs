using LibraryProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Domain.DTO
{
    public class AuthorDTO
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
