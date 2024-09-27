using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.DTO
{
    public class BooksDTO
    {
        public int bookID { get; set; }
        public string bookName { get; set; }
        public string bookAuthor { get; set; }
        public string bookDescription { get; set; }
        public int bookISBN { get; set; }
    }
}
