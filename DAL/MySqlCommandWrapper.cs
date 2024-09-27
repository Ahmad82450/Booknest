using MySql.Data.MySqlClient;
using CL.DALInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MySqlCommandWrapper : IDBCommandWrapper
    {
        private MySqlCommand _command;

        public MySqlCommandWrapper(MySqlCommand command)
        {
            _command = command;
        }

        public void ParametersAddWithValue(string parameterName, object value)
        {
            _command.Parameters.AddWithValue(parameterName, value);
        }

        public int ExecuteNonQuery()
        {
            return _command.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {

            return _command.ExecuteScalar();
        }

        public MySqlDataReader ExecuteReader()
        {
            return _command.ExecuteReader();
        }
    }
}
