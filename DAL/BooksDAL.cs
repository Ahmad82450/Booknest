using MySql.Data;
using MySql.Data.MySqlClient;
using CL.DTO;
using CL.DALInterface;
using System.Data;

namespace DAL
{
    public class BooksDAL : IBooksRepository
    {
        public readonly IDBConnection _conn;
        public BooksDAL(IDBConnection conn) 
        { 
            _conn = conn;
        }

        public List<BooksDTO> GetAllBooks()
        {
            List<BooksDTO> books = new List<BooksDTO>();
            try
            {
                _conn.Open();
                string query = "SELECT * FROM Books";
                IDBCommandWrapper cmd = _conn.CreateCommand(query);
                using (MySqlDataReader myReader = cmd.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        BooksDTO book = new BooksDTO
                        {
                            bookID = myReader.GetInt32("bookid"),
                            bookName = myReader.GetString("bookname"),
                            bookDescription = myReader.GetString("bookDescription"),
                            bookAuthor = myReader.GetString("bookAuthor"),
                            bookISBN = myReader.GetString("bookISBN")
                        };
                        books.Add(book);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally { _conn.Close(); }

            return books;
        }

        public BooksDTO GetBook(int bookID) 
        {
            BooksDTO book = new();
            try
            {
                _conn.Open();
                string query = "SELECT * FROM `Books` WHERE `bookID` = @bookID;";
                IDBCommandWrapper cmd = _conn.CreateCommand(query);
                cmd.ParametersAddWithValue("@bookID", bookID);
                using (MySqlDataReader myReader = cmd.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        myReader.Read();
                        book = new BooksDTO
                        {
                            bookID = myReader.GetInt32("bookID"),
                            bookName = myReader.GetString("bookName"),
                            bookDescription = myReader.GetString("bookDescription"),
                            bookAuthor = myReader.GetString("bookAuthor"),
                            bookISBN = myReader.GetString("bookISBN")
                        };
                    }
                    else
                    {
                        Console.WriteLine("No book was found with this ID");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally { _conn.Close(); }

            return book;
        }

        //public BooksDTO GetBook(int bookID)
        //{
        //    BooksDTO book = new();
        //    try
        //    {
        //        _conn.Open();
        //        string query = "SELECT * FROM `Books` WHERE `bookID` = @bookID;";
        //        IDBCommandWrapper cmd = _conn.CreateCommand(query);
        //        cmd.Parameters.AddWithValue("@bookID", bookID);

        //        using (MySqlDataReader myReader = cmd.ExecuteReader())
        //        {
        //            if (myReader.HasRows)
        //            {
        //                myReader.Read();
        //                book = new BooksDTO
        //                {
        //                    bookID = myReader.GetInt32("bookID"),
        //                    bookName = myReader.GetString("bookName"),
        //                    bookDescription = myReader.GetString("bookDescription"),
        //                    bookAuthor = myReader.GetString("bookAuthor"),
        //                    bookISBN = myReader.GetString("bookISBN")
        //                };
        //            }
        //            else
        //            {
        //                Console.WriteLine("No book found with the specified ID.");
        //            }
        //        }

        //        Console.WriteLine(book);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    finally
        //    {
        //        _conn.Close();
        //    }

        //    return book;
        //}

    }
}
