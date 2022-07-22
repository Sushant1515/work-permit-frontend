using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace WorkFlowRequestServices.Interface
{
   public interface IDBConnection
    {
        static SqlTransaction sqltrannew;
        static SqlConnection con;
        void OpenDBConnection();
        DataTable OpenDataTable(string cmdstr, DataTable xdt);
    }
}
