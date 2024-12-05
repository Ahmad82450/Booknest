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

        [TestMethod]
        public void GetBooks_Failed()
        {
            // Arrange
            var mockBookDAL = new Mock<IBooksRepository>();
            var mockMapper = new Mock<IMapper>();

            // Mock method calls to return null or empty for failure scenario
            mockBookDAL.Setup(dal => dal.GetAllBooks()).Returns((List<BooksDTO>)null);

            // Create the service instance
            var bookService = new BooksService(mockBookDAL.Object, mockMapper.Object);

            // Act
            var result = bookService.GetAllBooks();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetBook_Succeeded()
        {
            // Arrange
            var mockBookDAL = new Mock<IBooksRepository>();
            var mockMapper = new Mock<IMapper>();

            // Mock Database Response (BookDTO)
            var bookDTO = new BooksDTO
            {
                bookID = 1,
                bookName = "Test Bookname",
                bookAuthor = "Test Author",
                bookDescription = "Book Description",
                bookISBN = "123456789"
            };

            // Expected Service Response (Book)
            var book = new Book
            {
                bookID = bookDTO.bookID,
                bookName = bookDTO.bookName,
                bookAuthor = bookDTO.bookAuthor,
                bookDescription = bookDTO.bookDescription,
                bookISBN = bookDTO.bookISBN
            };

            // Mock method call
            mockBookDAL.Setup(dal => dal.GetBook(It.IsAny<int>())).Returns(bookDTO);
            mockMapper.Setup(mapper => mapper.Map<Book>(bookDTO)).Returns(book);

            // Create the service instance
            var bookService = new BooksService(mockBookDAL.Object, mockMapper.Object);

            // Act
            var result = bookService.GetBook(1);

            // Assert
            Assert.AreEqual(book.bookID, result.bookID);
            Assert.AreEqual(book.bookName, result.bookName);
        }

        [TestMethod]
        public void GetBook_Failed()
        {
            // Arrange
            var mockBookDAL = new Mock<IBooksRepository>();
            var mockMapper = new Mock<IMapper>();

            // Mock method call to return null for failure scenario
            mockBookDAL.Setup(dal => dal.GetBook(It.IsAny<int>())).Returns((BooksDTO)null);

            // Create the service instance
            var bookService = new BooksService(mockBookDAL.Object, mockMapper.Object);

            // Act
            var result = bookService.GetBook(1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SearchBooks_Succeeded()
        {
            // Arrange
            var mockBookDAL = new Mock<IBooksRepository>();
            var mockMapper = new Mock<IMapper>();

            // Mock Database Response (BooksDTO)
            var bookDTO = new BooksDTO
            {
                bookID = 1,
                bookName = "Test Bookname",
                bookAuthor = "Test Author",
                bookDescription = "Book Description",
                bookISBN = "123456789"
            };
            var booksDTOList = new List<BooksDTO> { bookDTO };

            // Expected Service Response (Book)
            var book = new Book
            {
                bookID = bookDTO.bookID,
                bookName = bookDTO.bookName,
                bookAuthor = bookDTO.bookAuthor,
                bookDescription = bookDTO.bookDescription,
                bookISBN = bookDTO.bookISBN
            };
            var bookModelList = new List<Book> { book };

            // Mock method calls
            mockBookDAL.Setup(dal => dal.SearchBooks(It.IsAny<string>())).Returns(booksDTOList);
            mockMapper.Setup(mapper => mapper.Map<List<Book>>(booksDTOList)).Returns(bookModelList);

            // Create the service instance
            var bookService = new BooksService(mockBookDAL.Object, mockMapper.Object);

            // Act
            var result = bookService.SearchBooks("Test");

            // Assert
            Assert.AreEqual(bookModelList.Count(), result.Count());
            Assert.AreEqual(bookModelList[0].bookName, result[0].bookName);
        }

        [TestMethod]
        public void SearchBooks_Failed()
        {
            // Arrange
            var mockBookDAL = new Mock<IBooksRepository>();
            var mockMapper = new Mock<IMapper>();

            // Mock method call to return an empty list for failure scenario
            mockBookDAL.Setup(dal => dal.SearchBooks(It.IsAny<string>())).Returns(new List<BooksDTO>());
            mockMapper.Setup(mapper => mapper.Map<List<Book>>(It.IsAny<List<BooksDTO>>())).Returns(new List<Book>());

            // Create the service instance
            var bookService = new BooksService(mockBookDAL.Object, mockMapper.Object);

            // Act
            var result = bookService.SearchBooks("NonExistent");

            // Assert
            Assert.AreEqual(0, result.Count());
        }

    }
}