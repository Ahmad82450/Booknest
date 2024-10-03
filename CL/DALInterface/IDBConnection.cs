using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.DALInterface
{
    public interface IDBConnection
    {
        public void Open();
        public void Close();
        public IDBCommandWrapper CreateCommand(string query);
    }
}
