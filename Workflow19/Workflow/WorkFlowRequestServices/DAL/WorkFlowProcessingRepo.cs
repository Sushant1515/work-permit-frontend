using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowRequestServices.Interface;
using WorkFlowRequestServices.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;
namespace WorkFlowRequestServices.Repository
{
   


    public class WorkFlowProcessingRepo : IWorkFlowProcess
    {
        private DataTable xdt = new DataTable();
        private SqlTransaction sqltran = null;
        DBConnectionRepo dbcon = new DBConnectionRepo();

        public int Generaterequest(ProcessRequest request, int processid)
        {
            int rows = -1;

            Int32 RequestID = 0;

            DataTable xdt = new DataTable();
            SqlTransaction sqltran = null;
            string sql = "select Max(iRequest_Id) as MaxReqID from Process_Request";
            dbcon.OpenDataTable(sql, xdt);
            RequestID = Convert.ToInt32(xdt.Rows[0]["MaxReqID"]) + 1;

            //for First generate Request
            using (SqlCommand cmd = new SqlCommand("SP_Create_Request", dbcon.con))
            {
                cmd.Connection = dbcon.con;
                sqltran = dbcon.con.BeginTransaction();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltran;
                cmd.Parameters.AddWithValue("@reqid", RequestID);
                cmd.Parameters.AddWithValue("@istatusid", request.iStatus_Id);
                dbcon.OpenDBConnection();
                rows = cmd.ExecuteNonQuery();
            }
            xdt.Clear();
            xdt.Dispose();
            
            // second genrate workflow instance process
            int wfid = 0;
            sql = "select iwf_ID FROM WorkFlow_Process_Mapping where iProcess_Id=" + processid;
            dbcon.OpenDataTable(sql, xdt);
            wfid = Convert.ToInt32(xdt.Rows[0]["iwf_ID"]);
            xdt.Clear();
            xdt.Dispose();

            using (SqlCommand cmd = new SqlCommand("SP_Create_Request", dbcon.con))
            {
                cmd.Connection = dbcon.con;
                sqltran = dbcon.con.BeginTransaction();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltran;
                cmd.Parameters.AddWithValue("@reqid", RequestID);
                cmd.Parameters.AddWithValue("@istatusid", request.iStatus_Id);
                dbcon.OpenDBConnection();
                rows = cmd.ExecuteNonQuery();
            }



            //end here






            if (rows > 0)
            {
                sqltran.Commit();
            }
            else
            {
                sqltran.Rollback();
            }
            return rows;

            
        }



        public async Task<int> CreateWorkFlowProcess(WorkFlowProcessMaster workflowProcess)
        {
            // Save in Table-> Process_Master0 
            int rows = -1;
            Int32 Maxid = 0;

            DataTable xdt = new DataTable();
            SqlTransaction sqltran = null;
            string sql = "select Max(iProcess_Id) as Maxid from Process_Master";
            dbcon.OpenDataTable(sql, xdt);
            Maxid = Convert.ToInt32(xdt.Rows[0]["Maxid"]) + 1;
            await using (SqlCommand cmd = new SqlCommand("SP_create_WF_Process", dbcon.con))
            {
                cmd.Connection = dbcon.con;
                sqltran = dbcon.con.BeginTransaction();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltran;

                cmd.Parameters.AddWithValue("@iProcess_Id", Maxid);
                cmd.Parameters.AddWithValue("@sProcess_Name", workflowProcess.sProcess_Name);
                cmd.Parameters.AddWithValue("@bActive", workflowProcess.bActive);
                dbcon.OpenDBConnection();
                rows = cmd.ExecuteNonQuery();
            }
            if (rows > 0)
            {
                sqltran.Commit();
            }
            else
            {
                sqltran.Rollback();
            }
            return rows;
        }

        public async Task<int> CreateWorkFlowProcessMapping(int WFId, WorkFlowProcessMaster workflowProcess, WorkFlowProcessMapping workFlowMapping, WorkFlowInstance workFlowInstance)
        {
            int rows = -1;
            Int32 MaxInsId = 0;

            sqltran = dbcon.con.BeginTransaction();
            // Save in Table-> WorkFlow_Process_Mapping
            await using (SqlCommand cmd = new SqlCommand("SP_WF_ProcessMapping", dbcon.con))
            {
                cmd.Connection = dbcon.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltran;

                cmd.Parameters.AddWithValue("@iWF_Id", WFId);
                cmd.Parameters.AddWithValue("@iProcess_Id", workflowProcess.iProcess_Id);
                cmd.Parameters.AddWithValue("@dCreated_Date", workFlowMapping.dCreated_Date);
                cmd.Parameters.AddWithValue("@iCreated_User_Cd", workFlowMapping.iCreated_User_Cd);
                dbcon.OpenDBConnection();
                rows = cmd.ExecuteNonQuery();
            }

            // Save in Table-> WorkFlow_Instance
            xdt = new DataTable();
            string sql = "select Max(iWF_Instance_Id) as MaxInsId from WorkFlow_Instance";
            dbcon.OpenDataTable(sql, xdt);
            MaxInsId = Convert.ToInt32(xdt.Rows[0]["MaxInsId"]) + 1;

            await using (SqlCommand cmd = new SqlCommand("SP_create_WF_Process_Instance", dbcon.con))
            {
                cmd.Connection = dbcon.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltran;

                cmd.Parameters.AddWithValue("@iWF_Instance_Id", MaxInsId);
                cmd.Parameters.AddWithValue("@iWF_Id", WFId);
                cmd.Parameters.AddWithValue("@iProcess_Id", workflowProcess.iProcess_Id);
                cmd.Parameters.AddWithValue("@iRequest_Id", workFlowInstance.iRequest_Id);
                cmd.Parameters.AddWithValue("@iCurrent_Step_Id", workFlowInstance.iCurrent_Step_Id);
                dbcon.OpenDBConnection();
                rows = cmd.ExecuteNonQuery();
            }

            if (rows > 0)
            {
                sqltran.Commit();
            }
            else
            {
                sqltran.Rollback();
            }
            return rows;
        }

        
    }
}
