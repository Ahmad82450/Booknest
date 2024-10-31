using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CL.DTO;

namespace CL.DALInterface
{
    public interface IBooksRepository
    {
        public List<BooksDTO> GetAllBooks();
        public BooksDTO GetBook(int bookID);
    }
}
