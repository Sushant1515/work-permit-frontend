using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowRequestServices.Interface;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WorkFlowRequestServices.Repository
{
    public class DBConnectionRepo : IDBConnection
    {
        private string sql;
        public SqlConnection con = new SqlConnection();
        public SqlTransaction tran;
        private DataTable xdt;
        private string connectionString;
        public DataTable OpenDataTable(string cmdstr, DataTable xdt)
        {
            xdt.Clear();
            OpenDBConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmdstr, con);
            da.SelectCommand.Transaction = tran;
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(xdt);
            da.Dispose();
            return xdt;

        }

        public void OpenDBConnection()
        {
            connectionString = "Data Source=DESKTOP-PSRF7EJ;Database=WorkFlow;User ID=sa;Password=mnt@123;";
            if (con.State != ConnectionState.Open)
            {
                con.ConnectionString = connectionString;
                con.Open();

            }
            throw new NotImplementedException();
        }
    }
}
