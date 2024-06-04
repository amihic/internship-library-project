using Data.Repositories;
using Domain.Interfaces;
using LibraryProject.Data.Repositories;
using LibraryProject.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Data.Extensions
{
    public static class DependencyConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IRentBookRepository, RentBookRepository>();
        }
    }
}
