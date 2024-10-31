using BLL.Models;
using CL.DALInterface;
using System.Net;
using AutoMapper;


namespace BLL
{
    public class BooksService
    {
        private readonly IBooksRepository _booksDAL;
        private readonly IMapper _mapper;
        public BooksService(IBooksRepository booksDAL, IMapper mapper) 
        { 
            _booksDAL = booksDAL;
            _mapper = mapper;
        }
        public List<Book> GetAllBooks()
        {
            var booksDTO = _booksDAL.GetAllBooks();
            var books = _mapper.Map<List<Book>>(booksDTO);
            Console.WriteLine(books);
            return books;
        }

        public Book GetBook(int bookID) 
        {
            var bookDTO = _booksDAL.GetBook(bookID);
            var book = _mapper.Map<Book>(bookDTO);
            return book;

        }
    }
}
