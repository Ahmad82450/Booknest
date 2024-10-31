using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using CL.DALInterface;

namespace BooknestAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class GetBooksController : ControllerBase
    {
        private readonly BooksService _booksService;
        public GetBooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet(Name = "GetBooks")]
        public IEnumerable<BLL.Models.Book> Get()
        {
            return _booksService.GetAllBooks();
        }

        [HttpGet("{bookID}", Name = "GetBookInfo")]
        public ActionResult<BLL.Models.Book> Get(int bookID)
        {
            var book = _booksService.GetBook(bookID);
            if (book == null) 
            {
                return NotFound();
            }
            return Ok(book);
        }
    }
}
