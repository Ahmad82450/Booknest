using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CL.DALInterface
{
    public interface IDBCommandWrapper
    {
        void ParametersAddWithValue(string parameterName, object value);
        int ExecuteNonQuery();
        public object ExecuteScalar();
        public MySqlDataReader ExecuteReader();
    }
}
