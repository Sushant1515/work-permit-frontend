using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Repositories;

namespace WorkFlowEntry.Repository
{
      public class DBRepository : IDatabaseRepository
    {
       
        public SqlConnection con = new SqlConnection();
        public SqlTransaction tran;
        private IConfiguration _config;
        
        public DBRepository(IConfiguration config)
        {
            _config = config;
        }
        
        public DataTable FillDatatable(string cmdstr, DataTable xdt)
        {
            xdt.Clear();
            con.Close();
            con.ConnectionString = this._config.GetConnectionString("dbCon");
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmdstr, con);
            da.SelectCommand.Transaction = tran;
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(xdt);
            da.Dispose();
            return xdt;
        }

       



    }
}
