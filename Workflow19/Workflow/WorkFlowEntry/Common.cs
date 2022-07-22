using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WorkFlowEntry
{
    public  class Common
    {
        public  SqlConnection con=new SqlConnection();
        public  string sql;
        public  SqlTransaction sqltran;
        public  string constr;
        //private IConfiguration _config;
        public SqlDataAdapter da = new SqlDataAdapter();
        public DataTable xdt;

        public Common()
        {
            constr = "Data Source=DESKTOP-SIH06PQ; Initial Catalog=Workflow; uid=sa; Password=mnt@123;";
            con.ConnectionString = constr;
            con.Open();
           
            
        }



    }
}
