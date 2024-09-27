using MySql.Data;
using MySql.Data.MySqlClient;
using CL.DTO;
using CL.DALInterface;

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
