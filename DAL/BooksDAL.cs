using MySql.Data;
using MySql.Data.MySqlClient;
using CL.DTO;
using CL.DALInterface;
using System.Data;

namespace DAL
{
    public class BooksDAL : IBooksDAL
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
                            bookID = myReader.GetInt32("bookID"),
                            bookName = myReader.GetString("bookName"),
                            bookDescription = myReader.GetString("bookDescription"),
                            bookAuthor = myReader.GetString("bookAuthor"),
                            bookISBN = myReader.GetInt32("bookISBN")
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

    }
}
