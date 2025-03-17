using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCBooks.Models;

namespace MVCBooks.Data
{
    public class MVCBooksContext : DbContext
    {
        public MVCBooksContext (DbContextOptions<MVCBooksContext> options)
            : base(options)
        {
        }

        public DbSet<MVCBooks.Models.Book> Book { get; set; } = default!;
    }
}
