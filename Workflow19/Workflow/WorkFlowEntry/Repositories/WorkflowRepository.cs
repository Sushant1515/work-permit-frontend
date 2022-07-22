using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using WorkFlowEntry.Repository;


namespace WorkFlowEntry.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private IDatabaseRepository _dBRepository;
        private IConfiguration _config;

        WorkFlowContext db = new WorkFlowContext();

        public WorkflowRepository(IDatabaseRepository dBRepository, IConfiguration config )
        {
            _dBRepository = dBRepository;
            _config = config;
        }
        public WorkFlow_Master Create(WorkFlow_Master workflow)
        {
            var Maxid = db.WorkFlow_Master.Max(maxid => maxid.nWF_Id);
            using (var WlCtx = new WorkFlowContext())
            {
                WorkFlow_Master wf = new WorkFlow_Master();
                wf.nWF_Id= Convert.ToInt32(Maxid);
                wf.sWF_Description = workflow.sWF_Description;
                wf.dWF_Create_Date = workflow.dWF_Create_Date;
                wf.iCreated_User_Cd = workflow.iCreated_User_Cd;
                wf.bActive = workflow.bActive;
                WlCtx.WorkFlow_Master.Add(wf);
                WlCtx.SaveChanges();
            }

                //string sql = "select Max(nWF_Id) as Maxid from WorkFlow_Master";
                // _dBRepository.FillDatatable(sql, xdt);

                // Maxid = Convert.ToInt32(xdt.Rows[0]["Maxid"]) + 1;
            //    // SqlConnection con = new SqlConnection();
            //    // Common.con.ConnectionString = this._config.GetConnectionString("dbCon");
            //    // con.Open();
            //    using (SqlCommand cmd = new SqlCommand("SP_create_WF",Common.con ))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Transaction = Common.sqltran;
            //    cmd.Parameters.AddWithValue("@iWF_Id", 25);
            //    cmd.Parameters.AddWithValue("@wf_desc", workflow.sWF_Description);
            //    cmd.Parameters.AddWithValue("@wf_date", workflow.dWF_Create_Date);
            //    cmd.Parameters.AddWithValue("@userid", workflow.iCreated_User_Cd);
            //    cmd.Parameters.AddWithValue("@bactive", workflow.bActive);
            //    Common.con.Open();
            //    row = cmd.ExecuteNonQuery();
            //    cmd.Dispose();
                
            //}
            return workflow;
        }

        public async Task<bool> Delete(int id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = this._config.GetConnectionString("dbCon");
            con.Open();
            await using (SqlCommand cmd = new SqlCommand("SP_Delete_WF", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Wf_id", id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            return true;
        }
        public async Task<bool> Edit(WorkFlow_Master workflow,int id)
        {
            int row = -1;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = this._config.GetConnectionString("dbCon");
            con.Open();
            await using (SqlCommand cmd = new SqlCommand("SP_Edit_WF", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Wf_id", id);
                cmd.Parameters.AddWithValue("@wf_desc", workflow.sWF_Description);
                cmd.Parameters.AddWithValue("@wf_date", workflow.dWF_Create_Date);
                row=cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            return true;
        }
        public async Task<List<WorkFlow_Master>> GetWLList(int id)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = this._config.GetConnectionString("dbCon");
            con.Open();
            List<WorkFlow_Master> WFlist = new List<WorkFlow_Master>();
            string sql = string.Empty;
            if (id == 0)
            {
                sql = "Select* from WorkFlow_Master";
            }
            else
            {
                sql = "Select* from WorkFlow_Master where nWF_Id=" + id;
            }
            await using (SqlCommand cmd = new SqlCommand(sql,con))
            {
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    WFlist.Add(new WorkFlow_Master()
                    {
                        nWF_Id = int.Parse(reader["nWf_Id"].ToString()),
                        sWF_Description = reader["sWF_Description"].ToString(),
                        iCreated_User_Cd = int.Parse(reader["iCreated_User_Cd"].ToString()),
                        dWF_Create_Date = DateTime.Parse(reader["dWF_Create_Date"].ToString()),
                        bActive = bool.Parse(reader["bActive"].ToString())
                    });
                }
            }
            return WFlist;
        }

        public async Task<List<WorkFlow_Instance>> GetWorkflowList(int id,int ipro_id, int ics_id)
        {
          
                SqlConnection con = new SqlConnection();
                con.ConnectionString = this._config.GetConnectionString("dbCon");

                con.Open();
                List<WorkFlow_Instance> WFlist = new List<WorkFlow_Instance>();
                string sql = string.Empty;

                if (id == 0)
                {
                    sql = "Select* from WorkFlow_Instance";
                }
                else
                {
                //sql = "Select * from WorkFlow_Instance where iWF_Id=" + id + " AND iCurrent_Step_Id=" + ics_id;
                sql = "Select * from WorkFlow_Instance where iWF_Id=" + id + " AND iProcess_Id=" + ipro_id + "AND iCurrent_Step_Id=" + ics_id;
            }
                await using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    IDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        WFlist.Add(new WorkFlow_Instance()
                        {
                            iWF_Instance_Id = int.Parse(reader["iWF_Instance_Id"].ToString()),
                            iWF_Id = int.Parse(reader["iWF_Id"].ToString()),
                            iProcess_Id = int.Parse(reader["iProcess_Id"].ToString()),
                            iRequest_Id = int.Parse(reader["iRequest_Id"].ToString()),
                            iCurrent_Step_Id = int.Parse(reader["iCurrent_Step_Id"].ToString()),
                            iNext_Step_Id = int.Parse(reader["iNext_Step_Id"].ToString()),
                            iCurrent_Step_Approval_Role_Id = int.Parse(reader["iCurrent_Step_Approval_Role_Id"].ToString()),
                            iStatus_Id = int.Parse(reader["iStatus_Id"].ToString()),
                            dCreated_Date = DateTime.Parse(reader["dCreated_Date"].ToString()),
                            dUpdated_Date = DateTime.Parse(reader["dUpdated_Date"].ToString()),

                        });
                    }
                }
                return WFlist;
            
            }

        public async Task<string> WF_Dtl_Post(int Req_id,int Step_id,int User_id)
        {
            try
            {

            
            var Fetch_User_Role = db.User_Master.Where(x => x.iUser_Cd == Convert.ToDecimal(User_id)).FirstOrDefault();

            if (Fetch_User_Role != null)

            {
                var Main = db.WorkFlow_Instance.Where(x => x.iRequest_Id == Req_id && x.iCurrent_Step_Id == Step_id ).FirstOrDefault();

                if (Main != null)
                {
                    WorkFlow_Instance_Details Detail = new WorkFlow_Instance_Details();
                    Detail.iTran_no = 4;
                    Detail.iWF_Instance_Id = Main.iWF_Instance_Id;
                    Detail.iStep_Id = Main.iCurrent_Step_Id;
                    Detail.iApproval_Role_Id =(short)Main.iCurrent_Step_Approval_Role_Id;
                    Detail.iApproval_User_Cd = Fetch_User_Role.iUser_Cd;
                    Detail.iStatus_Id = 2;
                    Detail.dCreated_Date = Main.dCreated_Date;
                    Detail.dUpdated_Date = Main.dUpdated_Date;
                    db.WorkFlow_Instance_Details.Add(Detail);
                    db.SaveChanges();
                    return "Data is inserted successfully";

                }
                else
                {
                    return "Role id is not match";
                }
            }
            else
            {
                return "User code is not found";
            }

            }
            catch(Exception ex)
            {
                return "sds";
            }

            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = this._config.GetConnectionString("dbCon");
            //con.Open();
            //await using (SqlCommand cmd = new SqlCommand("Fetch_WF", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@Pro_id", Pro_id);
            //    cmd.Parameters.AddWithValue("@User_id", User_id);
            //    cmd.ExecuteReader();
            //    cmd.Dispose();
            //}
            //return "Data is inserted successfully";
        }

    }
    }
