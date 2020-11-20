using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryForYou.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Book> Blogs { get; set; }
        public DbSet<Models.Customer> Customers { get; set; }
        public DbSet<Models.BorrowHistory> BorrowHistories { get; set; }
    }
}
