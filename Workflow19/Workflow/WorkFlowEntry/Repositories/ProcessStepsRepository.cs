using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using RulesEngine.Models;
using RulesEngine.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using System.Security.Cryptography;
using WorkFlowEntry.Services;


namespace WorkFlowEntry.Repositories
{
    public class ProcessStepsRepository : IProcessStepsRepository
    {
        private IDatabaseRepository _dBRepository;
        private readonly IEmailService _mailService;
        private IConfiguration _config;
        APIResponse responseData = new APIResponse();
        Common cmn = new Common();
        public ProcessStepsRepository(IDatabaseRepository dBRepository, IConfiguration config, IEmailService mailService)
        {
            _dBRepository = dBRepository;
            _config = config;
            _mailService = mailService;
        }
        public async Task<bool> ProcessStepStatus(int processid)
        {
            int wfid;
            string xtablename = string.Empty;
            string xfieldname= string.Empty;
            cmn.sql = "select iWF_Id from WorkFlow_Process_Mapping where iProcess_Id=" + processid;
            using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
            {
                wfid = Convert.ToInt32(cmd.ExecuteScalar());
            }

            //here will check current process step status get open/start status only
            WorkFlow_Instance currentprocess = new WorkFlow_Instance();
            cmn.sql = "select * from WorkFlow_Instance where iWf_id=" + wfid + "and iProcess_id=" + processid +" and iStatus_id<>2";
            using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
            {
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    currentprocess.iWF_Instance_Id= int.Parse(reader["iWF_Instance_Id"].ToString());
                    currentprocess.iCurrent_Step_Id = int.Parse(reader["iCurrent_Step_Id"].ToString());
                    currentprocess.iNext_Step_Id = int.Parse(reader["iNext_Step_Id"].ToString());
                    currentprocess.iCurrent_Step_Approval_Role_Id = Int16.Parse(reader["iCurrent_Step_Approval_Role_Id"].ToString());
                    currentprocess.iRequest_Id= int.Parse(reader["iRequest_Id"].ToString());
                    currentprocess.dCreated_Date= DateTime.Parse(reader["dCreated_Date"].ToString());
                }
                reader.Close();
            }
            //here will check current step's Approval status
            //bool bappvoedByAll = false;
            bool bIsCurrentStepApproved = false;
            //int iApprovalstatusId = 0;
            int xcntduration = 0;
            int maxidinstaceid = 0;
            //DateTime xcurrendate;
            //DateTime dCreatedDate;

            int xCurrentStepId = 0;
            int xNextStepID = 0;

            //Total approval count for current step of work flow
            int xTotalapprovalcount = 0;
            int xTotalapprovedcount = 0;
            cmn.sql = "select COUNT(*) AS ApprovalCount from WorkFlow_Role_Approvals where iWF_Id=" + wfid + " and iStep_id=" + currentprocess.iCurrent_Step_Id;
            using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
            {
                xTotalapprovalcount = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
            }
            cmn.sql = "select COUNT(*) AS TotalApprvedCount from WorkFlow_Instance_Details where iWf_Instance_id=" + currentprocess.iWF_Instance_Id + " and iStep_id=" + currentprocess.iCurrent_Step_Id;
            using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
            {
                xTotalapprovedcount = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
            }
            if(xTotalapprovedcount>=xTotalapprovalcount)
            {
                bIsCurrentStepApproved = true;
            }

            //end here
            cmn.sqltran = cmn.con.BeginTransaction();
            int rows;
            //xcurrendate = DateTime.Now;
            //xcntduration = Convert.ToInt32((xcurrendate - currentprocess.dCreated_Date).Days);
            //cmn.sql = "select iStatus_id,dCreated_Date from WorkFlow_Instance_Details where iWF_Instance_Id=" + currentprocess.iWF_Instance_Id + " and iStep_id=" + currentprocess.iCurrent_Step_Id + " and iApproval_Role_Id=" + currentprocess.iCurrent_Step_Approval_Role_Id;
            //using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
            //{
            //    IDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        iApprovalstatusId = int.Parse(reader["iStatus_id"].ToString());
            //        dCreatedDate = DateTime.Parse(reader["dCreated_Date"].ToString());
                    
            //        bIsCurrentStepApproved = true;
            //    }
            //    reader.Close();
            //}

            //here will apply if user not approved withind specified duration then send notification email or go to next role level
            //for same day not required and current step not approved then check escalation defination
            if (xcntduration > 0 && bIsCurrentStepApproved==false)
            {
                bool bIsEscalationruledefine = false;
                WF_Approval_Escalation_Defination ApprovalEscalationRule = new WF_Approval_Escalation_Defination();
                cmn.sql = "select * from WorkFlow_Approval_Escalaltion_Defination where iWf_Id=" + wfid + " and iStep_Id=" + currentprocess.iCurrent_Step_Id;
                using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
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
                        cmn.sqltran = cmn.con.BeginTransaction();
                        xtablename = _config.GetValue<string>("WorkFlowInstanceMax:Entityname");
                        xfieldname = _config.GetValue<string>("WorkFlowInstanceMax:Attributename");
                        using (SqlCommand cmd = new SqlCommand("select Max(" + xfieldname + ") from " + xtablename + "", cmn.con))
                        {
                            cmd.Transaction = cmn.sqltran;
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
                        //create work flow instance 
                        using (SqlCommand cmd = new SqlCommand("SP_create_WF_Process_Instance", cmn.con))
                        {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Transaction = cmn.sqltran;
                                cmd.Parameters.AddWithValue("@iWF_Instance_Id", maxidinstaceid);
                                cmd.Parameters.AddWithValue("@iWF_Id", wfid);
                                cmd.Parameters.AddWithValue("@iProcess_Id", processid);
                                cmd.Parameters.AddWithValue("@iRequest_Id", currentprocess.iRequest_Id);
                                cmd.Parameters.AddWithValue("@iCurrent_Step_Id", currentprocess.iCurrent_Step_Id);
                                cmd.Parameters.AddWithValue("@iNext_Step_Id", currentprocess.iNext_Step_Id);
                                cmd.Parameters.AddWithValue("@iCurrent_Step_Approval_Role_Id", ApprovalEscalationRule.iNext_Escalate_Role_Id);
                                cmd.Parameters.AddWithValue("@iStatus_Id", 1);
                                cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@updatedate", DateTime.Now);
                                rows = cmd.ExecuteNonQuery();
                                cmd.Dispose();
                        }
                        using (SqlCommand cmd = new SqlCommand("SP_Insert_WF_Instance_Details", cmn.con))
                        {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Transaction = cmn.sqltran;
                                cmd.Parameters.AddWithValue("@wfinstanceid", maxidinstaceid);
                                cmd.Parameters.AddWithValue("@stepid", currentprocess.iCurrent_Step_Id);
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
                    EmailRequest emailrequest = new EmailRequest();
                    emailrequest.ToEmail = ApprovalEscalationRule.sNext_Escalte_EmailId;
                    emailrequest.Subject = "Work Flow Process Apprval Role";
                    emailrequest.Body = "Please Approved Work Flow Process....";
                    await _mailService.SendEmailAsync(emailrequest);
                    //end here mail id
                }
            }
            //end here approval notification 
            //here will check current process step status with condition

            //get request object value from UI
            RequestObject xreqdata = new RequestObject();
            xreqdata.country = "eurpoe";
            xreqdata.state = "rajasthan";
            xreqdata.cityname = "udaipur";

            if (bIsCurrentStepApproved==true)
            {
                //List<WorkFlow_Step_Transition_Master> processstep = new List<WorkFlow_Step_Transition_Master>();
                WorkFlow_Step_Transition_Master processstep = new WorkFlow_Step_Transition_Master();
                string sCondition = string.Empty;
                DataTable xdt = new DataTable();
                cmn.sql = "select * from WorkFlow_Step_Transition_Master where iWf_id=" + wfid + " and iCurrent_Step_id=" + currentprocess.iCurrent_Step_Id;
                _dBRepository.FillDatatable(cmn.sql, xdt);
                foreach(DataRow row in xdt.Rows)
                {
                    processstep.iTransition_Id = int.Parse(row["iTransition_Id"].ToString());
                    processstep.iCurrent_Step_Id = int.Parse(row["iCurrent_Step_Id"].ToString());
                    processstep.iNext_Step_Id = int.Parse(row["iNext_Step_Id"].ToString());
                    processstep.sCondition = row["sCondition"].ToString();
                    Condition listcondtion = new Condition();
                    string xconditionvalue = string.Empty;
                    listcondtion = JsonConvert.DeserializeObject<Condition>(processstep.sCondition);
                    if (xreqdata.country == listcondtion.coutry)
                    {
                        //process goes to this step
                        xtablename = _config.GetValue<string>("WorkFlowInstanceMax:Entityname");
                        xfieldname = _config.GetValue<string>("WorkFlowInstanceMax:Attributename");

                        xCurrentStepId = processstep.iCurrent_Step_Id;
                        xNextStepID = processstep.iNext_Step_Id;

                       using (SqlCommand cmd = new SqlCommand("select Max(" + xfieldname + ") from " + xtablename + "", cmn.con))
                        {
                                cmd.Transaction = cmn.sqltran;
                                maxidinstaceid = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                                cmd.Dispose();
                        }

                        using (SqlCommand cmd = new SqlCommand("SP_create_WF_Process_Instance", cmn.con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Transaction = cmn.sqltran;
                            cmd.Parameters.AddWithValue("@iWF_Instance_Id", maxidinstaceid);
                            cmd.Parameters.AddWithValue("@iWF_Id", wfid);
                            cmd.Parameters.AddWithValue("@iProcess_Id", processid);
                            cmd.Parameters.AddWithValue("@iRequest_Id", currentprocess.iRequest_Id);
                            cmd.Parameters.AddWithValue("@iCurrent_Step_Id", processstep.iNext_Step_Id);
                            cmd.Parameters.AddWithValue("@iNext_Step_Id", processstep.iNext_Step_Id + 1);
                            cmd.Parameters.AddWithValue("@iCurrent_Step_Approval_Role_Id", currentprocess.iCurrent_Step_Approval_Role_Id);
                            cmd.Parameters.AddWithValue("@iStatus_Id", 1);
                            cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@updatedate", DateTime.Now);
                            rows = cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                        //here will update current step's status id also
                        //here 2=Completed
                        cmn.sql = "update WorkFlow_Instance set iStatus_Id=2 where iWF_Instance_Id=" + currentprocess.iWF_Instance_Id;
                        using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Transaction = cmn.sqltran;
                            rows = cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                        //end here
                        cmn.sqltran.Commit();
                        break;
                    }
                }

                ///end here for each loop




                //using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
                //{
                    
                //    IDataReader reader = cmd.ExecuteReader();
                //    while (reader.Read())
                //    {
                //        //processstep.Add(new WorkFlow_Step_Transition_Master()
                //        //{
                //        // iTransition_Id = int.Parse(reader["iTransition_Id"].ToString()),
                //        // iCurrent_Step_Id = int.Parse(reader["iCurrent_Step_Id"].ToString()),
                //        // iNext_Step_Id = int.Parse(reader["iNext_Step_Id"].ToString()),
                //        // sCondition = reader["sCondition"].ToString()
                //        processstep.iTransition_Id = int.Parse(reader["iTransition_Id"].ToString());
                //        processstep.iCurrent_Step_Id = int.Parse(reader["iCurrent_Step_Id"].ToString());
                //        processstep.iNext_Step_Id = int.Parse(reader["iNext_Step_Id"].ToString());
                //        processstep.sCondition = reader["sCondition"].ToString();
                //        Condition listcondtion = new Condition();
                //        string xconditionvalue = string.Empty;
                //        listcondtion = JsonConvert.DeserializeObject<Condition>(processstep.sCondition);
                //        if (xreqdata.country == listcondtion.coutry)
                //        {
                //            //process goes to this step
                //            xtablename = _config.GetValue<string>("WorkFlowInstanceMax:Entityname");
                //            xfieldname = _config.GetValue<string>("WorkFlowInstanceMax:Attributename");

                //            xCurrentStepId = processstep.iCurrent_Step_Id;
                //            xNextStepID = processstep.iNext_Step_Id;

                //            try
                //            {
                               
                //                using (SqlCommand cmd1 = new SqlCommand("select Max(" + xfieldname + ") from " + xtablename + "", cmn.con))
                //                {
                //                    cmd1.Transaction = cmn.sqltran;
                //                    maxidinstaceid = Convert.ToInt32(cmd1.ExecuteScalar()) + 1;
                //                    cmd1.Dispose();
                //                }
                                
                //            }
                //            catch (Exception ex)
                //            {

                //                throw;
                //            }

                           
                //            using (SqlCommand cmd1 = new SqlCommand("SP_create_WF_Process_Instance", cmn.con))
                //            {

                //                cmd1.CommandType = CommandType.StoredProcedure;
                //                cmd1.Transaction = cmn.sqltran;
                //                cmd1.Parameters.AddWithValue("@iWF_Instance_Id", maxidinstaceid);
                //                cmd1.Parameters.AddWithValue("@iWF_Id", wfid);
                //                cmd1.Parameters.AddWithValue("@iProcess_Id", processid);
                //                cmd1.Parameters.AddWithValue("@iRequest_Id", currentprocess.iRequest_Id);
                //                cmd1.Parameters.AddWithValue("@iCurrent_Step_Id", processstep.iNext_Step_Id);
                //                cmd1.Parameters.AddWithValue("@iNext_Step_Id", processstep.iNext_Step_Id + 1);
                //                cmd1.Parameters.AddWithValue("@iCurrent_Step_Approval_Role_Id", currentprocess.iCurrent_Step_Approval_Role_Id);
                //                cmd1.Parameters.AddWithValue("@iStatus_Id", 1);
                //                cmd1.Parameters.AddWithValue("@createddate", DateTime.Now);
                //                cmd1.Parameters.AddWithValue("@updatedate", DateTime.Now);
                //                rows = cmd1.ExecuteNonQuery();
                //                cmd.Dispose();



                //            }
                //            //here will update current step's status id also
                //            //here 2=Completed
                //            cmn.sql = "update WorkFlow_Instance set iStatus_Id=2 where iWF_Instance_Id=" + currentprocess.iWF_Instance_Id;
                //            using (SqlCommand cmd2 = new SqlCommand(cmn.sql, cmn.con))
                //            {
                //                cmd2.CommandType = CommandType.Text;
                //                cmd2.Transaction = cmn.sqltran;
                //                rows = cmd.ExecuteNonQuery();
                //                cmd.Dispose();
                //            }
                //            //end here
                //            cmn.sqltran.Commit();
                //            break;
                //        }



                //    }
                //    reader.Close();
                //}

                //List<Condition> listcondtion = new List<Condition>();
                //listcondtion = JsonConvert.DeserializeObject<List<Condition>>(processstep.sCondition);

               


                //end here
                //here will check multiple conditions here also

                //string jsondata=JsonConvert.SerializeObject(processstep.Select(a => a.sCondition));
                //foreach (var xobjvalue in processstep)
                //{
                //    Condition listcondtion = new Condition();
                //    string xconditionvalue = string.Empty;
                //    listcondtion = JsonConvert.DeserializeObject<Condition>(xobjvalue.sCondition);

                //    if (xreqdata.country == listcondtion.coutry)
                //    {
                //        //process goes to this step
                //        xtablename = _config.GetValue<string>("WorkFlowInstanceMax:Entityname");
                //        xfieldname = _config.GetValue<string>("WorkFlowInstanceMax:Attributename");

                //        xCurrentStepId = xobjvalue.iCurrent_Step_Id;
                //        xNextStepID = xobjvalue.iNext_Step_Id;


                //        cmn.sqltran = cmn.con.BeginTransaction();
                //        using (SqlCommand cmd = new SqlCommand("select Max(" + xfieldname + ") from " + xtablename + "", cmn.con))
                //        {
                //            cmd.Transaction = cmn.sqltran;
                //            maxidinstaceid = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                //        }
                //        using (SqlCommand cmd = new SqlCommand("SP_create_WF_Process_Instance", cmn.con))
                //        {

                //            cmd.CommandType = CommandType.StoredProcedure;
                //            cmd.Transaction = cmn.sqltran;
                //            cmd.Parameters.AddWithValue("@iWF_Instance_Id", maxidinstaceid);
                //            cmd.Parameters.AddWithValue("@iWF_Id", wfid);
                //            cmd.Parameters.AddWithValue("@iProcess_Id", processid);
                //            cmd.Parameters.AddWithValue("@iRequest_Id", currentprocess.iRequest_Id);
                //            cmd.Parameters.AddWithValue("@iCurrent_Step_Id", xobjvalue.iNext_Step_Id);
                //            cmd.Parameters.AddWithValue("@iNext_Step_Id", xobjvalue.iNext_Step_Id + 1);
                //            cmd.Parameters.AddWithValue("@iCurrent_Step_Approval_Role_Id", currentprocess.iCurrent_Step_Approval_Role_Id);
                //            cmd.Parameters.AddWithValue("@iStatus_Id", 1);
                //            cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
                //            cmd.Parameters.AddWithValue("@updatedate", DateTime.Now);
                //            rows = cmd.ExecuteNonQuery();
                //            cmd.Dispose();



                //        }
                //        //here will update current step's status id also
                //        //here 2=Completed
                //        cmn.sql = "update WorkFlow_Instance set iStatus_Id=2 where iWF_Instance_Id=" + currentprocess.iWF_Instance_Id;
                //        using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
                //        {
                //            cmd.CommandType = CommandType.Text;
                //            cmd.Transaction = cmn.sqltran;
                //            rows = cmd.ExecuteNonQuery();
                //            cmd.Dispose();
                //        }
                //        //end here
                //        cmn.sqltran.Commit();
                //        break;
                //    }
                //}
                //end here multiple condition



                //here check currentstep condtion with rulesengine
                var rp1 = new RuleParameter("basicInfo", xreqdata);
                var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "WorkflowRules.json", SearchOption.AllDirectories);
                if (files == null || files.Length == 0)
                    responseData.ResponseMessage = "Rules not found";
                var jsondata = System.IO.File.ReadAllText(files[0]);
                try
                {
                    var workflowrules = JsonConvert.DeserializeObject<List<RulesEngine.Models.Workflow>>(jsondata);
                    var re = new RulesEngine.RulesEngine(workflowrules.ToArray(), null);
                    var resultList = await re.ExecuteAllRulesAsync("DiscountWithCustomInputNames", rp1);
                }
                catch (Exception ex)
                {

                    throw;
                }
               
                
                //resultList.OnSuccess((eventName) =>
                //{
                //    //do write logic here
                //    if (iApprovalstatusId > 0)
                //    {
                //        int rows;
                //        //here move to next step of current process
                //        string xtablename = string.Empty;
                //        string xfieldname = string.Empty;
                //        int maxidinstaceid = 0;
                //        xtablename = _config.GetValue<string>("WorkFlowInstanceMax:Entityname");
                //        xfieldname = _config.GetValue<string>("WorkFlowInstanceMax:Attributename");
                //        cmn.sqltran = cmn.con.BeginTransaction();
                //        using (SqlCommand cmd = new SqlCommand("select Max(" + xfieldname + ") from " + xtablename + "", cmn.con))
                //        {
                //            cmd.Transaction = cmn.sqltran;
                //            maxidinstaceid = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                //        }
                //        using (SqlCommand cmd = new SqlCommand("SP_create_WF_Process_Instance", cmn.con))
                //        {
                //            cmd.CommandType = CommandType.StoredProcedure;
                //            cmd.Transaction = cmn.sqltran;
                //            cmd.Parameters.AddWithValue("@iWF_Instance_Id", maxidinstaceid);
                //            cmd.Parameters.AddWithValue("@iWF_Id", wfid);
                //            cmd.Parameters.AddWithValue("@iProcess_Id", processid);
                //            cmd.Parameters.AddWithValue("@iRequest_Id", currentprocess.iRequest_Id);
                //            cmd.Parameters.AddWithValue("@iCurrent_Step_Id", xCurrentStepId);
                //            cmd.Parameters.AddWithValue("@iNext_Step_Id", xNextStepID);
                //            cmd.Parameters.AddWithValue("@iCurrent_Step_Approval_Role_Id", 2);
                //            cmd.Parameters.AddWithValue("@iStatus_Id", 1);
                //            cmd.Parameters.AddWithValue("@createddate", DateTime.Now);
                //            cmd.Parameters.AddWithValue("@updatedate", DateTime.Now);
                //            rows = cmd.ExecuteNonQuery();
                //            cmd.Dispose();
                //        }

                //        //here will update current step's status id also
                //        //here 2=Completed
                //        cmn.sql = "update WorkFlow_Instance set iStatus_Id=2 where iWF_Instance_Id=" + currentprocess.iWF_Instance_Id;
                //        using (SqlCommand cmd = new SqlCommand(cmn.sql, cmn.con))
                //        {
                //            cmd.CommandType = CommandType.Text;
                //            cmd.Transaction = cmn.sqltran;
                //            rows = cmd.ExecuteNonQuery();
                //            cmd.Dispose();
                //        }
                //        //end here

                //        cmn.sqltran.Commit();
                //        responseData.ResponseMessage = eventName;
                //    }

                //});
                //resultList.OnFail(() =>
                //{
                //    //do write logi here  
                //    responseData.ResponseMessage = "No Condition Match";

                //});
            }
   














            //end here


            throw new NotImplementedException();
        }

        
    }
}
