using MySql.Data;
using MySql.Data.MySqlClient;
using CL.DTO;
using CL.DALInterface;
using System.Data;

namespace DAL
{
    public class BooksDAL : IBooksDAL
    {
        MySql.Data.MySqlClient.MySqlConnection? myConnection;
        private string connectionString = "";

        public List<BooksDTO> GetAllBooks()
        {
            List<BooksDTO> books = new List<BooksDTO>();
            try
            {
                connectionString = "server=127.0.0.1;uid=root;pwd=12345;database=test";
                myConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                myConnection.Open();

                MySqlCommand myCommand = new MySqlCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandText = @"SELECT * FROM Books";

                using var myReader = myCommand.ExecuteReader();

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally { myConnection.Close(); }

            return books;
        }

    }
}
