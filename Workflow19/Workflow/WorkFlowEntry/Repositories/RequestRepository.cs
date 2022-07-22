using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using System.Configuration;

namespace WorkFlowEntry.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private IDatabaseRepository _dBRepository;
        private IConfiguration _config;
        
        Common cmn = new Common();
        public RequestRepository(IDatabaseRepository dBRepository, IConfiguration config)
        {
            _dBRepository = dBRepository;
            _config = config;
            

        }
        public Process_Request CreateRequestProcess(Process_Request request, int processid)
        {
            int rows=-1;
            cmn.sqltran = cmn.con.BeginTransaction();
            int maxidreqid = 0;
            string xtablename = string.Empty;
            string xfieldname = string.Empty;

            xtablename = _config.GetValue<string>("RequestMaxDatastring:Entityname");
            xfieldname = _config.GetValue<string>("RequestMaxDatastring:Attributename");

            using (SqlCommand cmd = new SqlCommand("select Max(" + xfieldname + ") from " + xtablename + "", cmn.con))
            {
                cmd.Transaction = cmn.sqltran;
                maxidreqid = Convert.ToInt32(cmd.ExecuteScalar())+1;
            }
            ////update in Process request table
            using (SqlCommand cmd = new SqlCommand("SP_Create_Request", cmn.con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = cmn.sqltran;
                cmd.Parameters.AddWithValue("@reqid", maxidreqid);
                cmd.Parameters.AddWithValue("@istatusid", 1);
                cmd.Parameters.AddWithValue("@createdate", DateTime.Now);
                cmd.Parameters.AddWithValue("@updatedate", DateTime.Now);
                rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            //for workflow instance table
            int wfid = 0, icurrentsteproleid = 0, icurrentstepid = 0, inextstepid = 0;
            cmn.sql = "select iWF_Id from WorkFlow_Process_Mapping where iProcess_Id=" + processid;
            using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
            {
                cmd.Transaction = cmn.sqltran;
                wfid = Convert.ToInt32(cmd.ExecuteScalar());
            }

            //get step details
            cmn.sql = "SELECT TOP (1) A.iWF_Id, A.iStep_Id, A.iStep_Sequence, B.iRole_Id FROM WorkFlow_Step_Details AS A INNER JOIN WorkFlow_Role_Approvals AS B ON A.iWF_Id = B.iWF_Id AND A.iStep_Id = B.iStep_Id" +
                      " WHERE A.iWF_Id = " + wfid + " ORDER BY A.iStep_Id";
            using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
            {
                cmd.Transaction = cmn.sqltran;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    icurrentstepid = int.Parse(reader["iStep_Id"].ToString());
                    icurrentsteproleid = int.Parse(reader["iRole_Id"].ToString());
                    inextstepid = icurrentstepid + 1;
                }
                reader.Close();
            }
            int maxidinstaceid = 0;
            xtablename = _config.GetValue<string>("WorkFlowInstanceMax:Entityname");
            xfieldname = _config.GetValue<string>("WorkFlowInstanceMax:Attributename");

            using (SqlCommand cmd = new SqlCommand("select Max(" + xfieldname + ") from " + xtablename + "", cmn.con))
            {
                cmd.Transaction = cmn.sqltran;
                object obj = cmd.ExecuteScalar();
                if(obj==DBNull.Value)
                {
                    maxidinstaceid =  1;
                }
                else
                {
                    maxidinstaceid = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                }
            }
            
            //create work flow instance 
            using (SqlCommand cmd = new SqlCommand("SP_create_WF_Process_Instance", cmn.con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = cmn.sqltran;
                cmd.Parameters.AddWithValue("@iWF_Instance_Id",maxidinstaceid );
                cmd.Parameters.AddWithValue("@iWF_Id", wfid);
                cmd.Parameters.AddWithValue("@iProcess_Id", processid);
                cmd.Parameters.AddWithValue("@iRequest_Id", maxidreqid);
                cmd.Parameters.AddWithValue("@iCurrent_Step_Id", icurrentstepid);
                cmd.Parameters.AddWithValue("@iNext_Step_Id", inextstepid);
                cmd.Parameters.AddWithValue("@iCurrent_Step_Approval_Role_Id", icurrentsteproleid);
                cmd.Parameters.AddWithValue("@iStatus_Id", 1);
                cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
                cmd.Parameters.AddWithValue("@updatedate", DateTime.Now);
                rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                
            }


            if (rows > 0)
            {
                cmn.sqltran.Commit();
            }
            else
            {
                cmn.sqltran.Rollback();
            }
            return request;
        }

        public bool RequestProcessDefination(int Processid)
        {
            string sql = string.Empty;
            DataTable xdt=new DataTable();
            bool bProcessDefinationDefine = false;
            int wfid = 0;
            //process mapping check
            sql = "select iWF_id from WorkFlow_Process_Mapping where iprocess_id=" + Processid;
            _dBRepository.FillDatatable(sql, xdt);
            if(xdt.Rows.Count>0)
            {
                bProcessDefinationDefine = true;
                wfid = Convert.ToInt32(xdt.Rows[0]["iWF_id"]);
            }
            else
            {
                return false;
            }
            xdt.Clear();
            xdt.Dispose();
            //approval level check
            sql = "select nTran_id from WorkFlow_Role_Approvals where iWF_ID=" + wfid;
            _dBRepository.FillDatatable(sql, xdt);
            if (xdt.Rows.Count > 0)
            {
                bProcessDefinationDefine = true;
            }
            else
            {
                bProcessDefinationDefine = false;
            }
            return bProcessDefinationDefine;
        }

        public async Task<List<WorkFlowRoleApprovals>> WFApprovalList(int processid)
        {
            List<WorkFlowRoleApprovals> lstApproval = new List<WorkFlowRoleApprovals>();
            int wfid = 0;
            string sql = string.Empty;
            DataTable xdt = new DataTable();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = this._config.GetConnectionString("dbCon");
            con.Open();
            sql = "select iWF_Id from WorkFlow_Process_Mapping where iProcess_Id =" +processid;
            _dBRepository.FillDatatable(sql,xdt);
            wfid = Convert.ToInt32(xdt.Rows[0]["iWF_Id"]);
            sql = " SELECT A.iWF_Id, A.iStep_Id, A.iRole_Id, B.sRole_Desc FROM WorkFlow_Role_Approvals AS A INNER JOIN Role_Master AS B ON A.iRole_Id = B.nRole_Id" +
                 " WHERE A.iWF_Id = " + wfid;
           await using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstApproval.Add(new WorkFlowRoleApprovals()
                    {
                        iWF_Id = int.Parse(reader["iWF_Id"].ToString()),
                        iStep_Id = int.Parse(reader["iStep_Id"].ToString()),
                        iRole_id = int.Parse(reader["iRole_Id"].ToString()),
                        sRole_Desc = reader["sRole_Desc"].ToString()
                    });
                }
            }
            return lstApproval;

        }
    }
}
