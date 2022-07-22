using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using WorkFlowEntry.Repositories;

namespace WorkFlowEntry.Services
{
    public class TaskEscalation : IJob
    {
        Common _cmn;
        private IDatabaseRepository _dBRepository;
        private IConfiguration _config;


        public TaskEscalation(Common cmn, IDatabaseRepository dBRepository, IConfiguration config)
        {
            _cmn = cmn;
            _dBRepository = dBRepository;
            _config = config;
        }
        public Task Execute(IJobExecutionContext context)
        {

            var task = Task.Run(() => EscalationRuleCheck()); ;
            return task;


        }

        public  void EscalationRuleCheck()
        {
            DataTable xdt = new DataTable();
            _cmn.sql = " SELECT A.iRequest_Id, B.iWF_Id, B.iWF_Instance_Id, B.iProcess_Id, B.iCurrent_Step_Id,A.dCreated_Date,B.iCurrent_Step_Approval_Role_Id FROM Process_Request AS A INNER JOIN WorkFlow_Instance AS B ON A.iRequest_Id = B.iRequest_Id" +
                       " WHERE(A.iStatus_Id <> 1) ORDER BY A.iRequest_Id";
            _dBRepository.FillDatatable(_cmn.sql, xdt);

            DateTime xcurrendate= DateTime.Now;
            int xcntduration = 0;
            DateTime dCreatedDate;
            int cnt = 0;
            int maxidinstaceid = 0;
            string xtablename = string.Empty;
            string xfieldname = string.Empty;
            int rows;

            foreach (DataRow row in xdt.Rows)
            {
                dCreatedDate = Convert.ToDateTime(row["dCreated_Date"]);
                xcntduration = Convert.ToInt32((xcurrendate -dCreatedDate).Days);

                //here check current step approval for current role id 
                _cmn.sql = "select count(*) as cnt from WorkFlow_Instance_Details where iWf_Instance_id=" + row["iWF_Instance_Id"] + " and iStep_id=" + row["iCurrent_Step_Id"] + " and iApproval_Role_Id=" + row["iCurrent_Step_Approval_Role_Id"];
                using (SqlCommand cmd = new SqlCommand(_cmn.sql, _cmn.con))
                {
                    cnt = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                }
                if (xcntduration > 0 && cnt>0)
                {
                    bool bIsEscalationruledefine = false;
                    WF_Approval_Escalation_Defination ApprovalEscalationRule = new WF_Approval_Escalation_Defination();
                    _cmn.sql = "select * from WorkFlow_Approval_Escalaltion_Defination where iWf_Id=" + row["iWF_Id"] + " and iStep_Id=" + row["iCurrent_Step_Id"];
                    using (SqlCommand cmd = new SqlCommand(_cmn.sql, _cmn.con))
                    {
                        IDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ApprovalEscalationRule.iDuration_Start = int.Parse(reader["iDuration_Start"].ToString());
                            ApprovalEscalationRule.iDuration_End = int.Parse(reader["iDuration_End"].ToString());
                            ApprovalEscalationRule.iEscalate_Role_Id = int.Parse(reader["iEscalate_Role_Id"].ToString());
                            ApprovalEscalationRule.iNext_Escalate_Role_Id = int.Parse(reader["iNext_Escalate_Role_Id"].ToString());
                            ApprovalEscalationRule.sNext_Escalte_EmailId = reader["sNext_Escalte_EmailId"].ToString();
                            bIsEscalationruledefine = true;
                        }
                        reader.Close();
                    }
                    if (bIsEscalationruledefine == true)
                    {
                        bool escalate = Enumerable.Range(ApprovalEscalationRule.iDuration_Start, ApprovalEscalationRule.iDuration_End).Contains(xcntduration);
                        if (escalate == false)
                        {
                            maxidinstaceid = 0;
                            _cmn.sqltran = _cmn.con.BeginTransaction();
                            xtablename = _config.GetValue<string>("WorkFlowInstanceMax:Entityname");
                            xfieldname = _config.GetValue<string>("WorkFlowInstanceMax:Attributename");
                            using (SqlCommand cmd = new SqlCommand("select Max(" + xfieldname + ") from " + xtablename + "", _cmn.con))
                            {
                                cmd.Transaction = _cmn.sqltran;
                                object obj = cmd.ExecuteScalar();
                                if (obj == DBNull.Value)
                                {
                                    maxidinstaceid = 1;
                                }
                                else
                                {
                                    maxidinstaceid = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                                }
                            }
                            ////create work flow instance 
                            //using (SqlCommand cmd = new SqlCommand("SP_create_WF_Process_Instance", _cmn.con))
                            //{
                            //    cmd.CommandType = CommandType.StoredProcedure;
                            //    cmd.Transaction = _cmn.sqltran;
                            //    cmd.Parameters.AddWithValue("@iWF_Instance_Id", maxidinstaceid);
                            //    cmd.Parameters.AddWithValue("@iWF_Id", wfid);
                            //    cmd.Parameters.AddWithValue("@iProcess_Id", processid);
                            //    cmd.Parameters.AddWithValue("@iRequest_Id", currentprocess.iRequest_Id);
                            //    cmd.Parameters.AddWithValue("@iCurrent_Step_Id", currentprocess.iCurrent_Step_Id);
                            //    cmd.Parameters.AddWithValue("@iNext_Step_Id", currentprocess.iNext_Step_Id);
                            //    cmd.Parameters.AddWithValue("@iCurrent_Step_Approval_Role_Id", ApprovalEscalationRule.iNext_Escalate_Role_Id);
                            //    cmd.Parameters.AddWithValue("@iStatus_Id", 1);
                            //    cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
                            //    cmd.Parameters.AddWithValue("@updatedate", DateTime.Now);
                            //    rows = cmd.ExecuteNonQuery();
                            //    cmd.Dispose();
                            //}
                            using (SqlCommand cmd = new SqlCommand("SP_Insert_WF_Instance_Details", _cmn.con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Transaction = _cmn.sqltran;
                                cmd.Parameters.AddWithValue("@wfinstanceid", row["iWF_Instance_Id"]);
                                cmd.Parameters.AddWithValue("@stepid", row["iCurrent_Step_Id"]);
                                cmd.Parameters.AddWithValue("@ApprovalroleId", ApprovalEscalationRule.iNext_Escalate_Role_Id);
                                cmd.Parameters.AddWithValue("@Approvalusercd", 1);
                                cmd.Parameters.AddWithValue("@istatusid", 1);
                                cmd.Parameters.AddWithValue("@createdate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@updatedate", DateTime.Now);
                                rows = cmd.ExecuteNonQuery();
                                cmd.Dispose();
                            }
                        }
                        //mail generate for next role id
                        //EmailRequest emailrequest = new EmailRequest();
                        //emailrequest.ToEmail = ApprovalEscalationRule.sNext_Escalte_EmailId;
                        //emailrequest.Subject = "Work Flow Process Apprval Role";
                        //emailrequest.Body = "Please Approved Work Flow Process....";
                        //await _mailService.SendEmailAsync(emailrequest);
                        //end here mail id
                    }
                }
                //end here approval notification 




            }







        }

    }
}
