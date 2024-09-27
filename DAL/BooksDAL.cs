using MySql.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class BooksDAL
    {
        //MySql.Data.MySqlClient.MySqlConnection myConnection;
        //private string connectionString = "";

        //public void GetAllBooks()
        //{
        //    try
        //    {
        //        connectionString = "server=127.0.0.1;uid=root;pwd=12345;database=test";

        //        myConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

        //        myConnection.Open();

        //        MySqlCommand myCommand = new MySqlCommand();
        //        myCommand.Connection = myConnection;
        //        myCommand.CommandText = @"SELECT * FROM Books";

        //        using var myReader = myCommand.ExecuteReader();

        //        while (myReader.Read())
        //        {

        //        }
        //    };

        //}

    }
}
