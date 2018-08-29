using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WindowsFormsApp1
{
    public class BookContext : DbContext
    {
        public BookContext()
            :base("DefaultConnection")
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Sage> Sages { get; set; }
    }
}
