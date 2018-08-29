using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsApp1
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1000, 2017)]
        public int YearOfPublication { get; set;}
        public string Description { get; set; }
       

        [Required]
        public virtual ICollection<Sage> Sages { get; set; }
        public Book()
        {
            Sages = new List<Sage>();
        }
    }

    public class Sage
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 200)]
        public int Age { get; set; }
        public string City { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public Sage()
        {
            Books = new List<Book>();
        }
    }

}
