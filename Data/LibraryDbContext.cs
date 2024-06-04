using LibraryProject.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Data
{
    public class LibraryDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<RentBook> Rents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RentBook>()
                .HasOne(rb => rb.Book)
                .WithMany(b => b.Rents)
                .HasForeignKey(rb => rb.BookId)
                .IsRequired();

            modelBuilder.Entity<RentBook>()
                .HasOne(rb => rb.User)
                .WithMany(u => u.Rents)
                .HasForeignKey(rb => rb.UserId)
                .IsRequired();

            var adminUser = new User
            {
                Id = "userId",
                FirstName = "Admin",
                LastName = "Adminic",
                UserName = "admin@valcon.com",
                NormalizedUserName = "ADMIN@VALCON.COM",
                Email = "admin@valcon.com",
                NormalizedEmail = "ADMIN@VALCON.COM",
                EmailConfirmed = true
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "passworD1!");

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "adminId",
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<User>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "adminId",
                UserId = adminUser.Id
            });
        }
    }
}
