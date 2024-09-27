using MySql.Data.MySqlClient;
using CL.DALInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBconnection : IDBConnection
    {
        private string connectionString = "server=localhost;port=3306;database=proftaaksemester2;user=root;password=";

        public MySqlConnection connection { get; private set; }

        public DBconnection()
        {
            connection = new MySqlConnection(connectionString);
        }

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public IDBCommandWrapper CreateCommand(string query)
        {
            return new MySqlCommandWrapper(new MySqlCommand(query, connection));
        }
    }
}
