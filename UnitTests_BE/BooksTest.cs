using Moq;
using CL.DTO;
using CL.DALInterface;
using BLL;
using AutoMapper;
using BLL.Models;

namespace UnitTests_BE
{
    [TestClass]
    public class BooksTest
    {
        [TestMethod]
        public void GetBooks_Succeded()
        {
            // Arrange
            var mockUserDAL = new Mock<IBooksRepository>();
            var mockMapper = new Mock<IMapper>();

            //Database Response
            var bookDTO = new BooksDTO {
                bookID = 0,
                bookName = "Test Bookname",
                bookAuthor = "test author",
                bookDescription = "book Description",
                bookISBN = "123456789",
            };

            var booksDTOList = new List<BooksDTO> {bookDTO};

            //Service Response
            var book = new Book {
                bookID = bookDTO.bookID,
                bookName = bookDTO.bookName,
                bookAuthor = bookDTO.bookAuthor,
                bookDescription = bookDTO.bookDescription,
                bookISBN = bookDTO.bookISBN,
            };

            var bookModelList = new List<Book> {book};

            mockUserDAL.Setup(dal => dal.GetAllBooks()).Returns(booksDTOList);
            mockMapper.Setup(mapper => mapper.Map<List<Book>>(booksDTOList)).Returns(bookModelList);

            var bookService = new BooksService(mockUserDAL.Object, mockMapper.Object);

            var result = bookService.GetAllBooks();

            // Assert
            Assert.AreEqual(bookModelList.Count(), result.Count());
            Assert.AreEqual(bookModelList[0].bookName, result[0].bookName);
        }
    }
}