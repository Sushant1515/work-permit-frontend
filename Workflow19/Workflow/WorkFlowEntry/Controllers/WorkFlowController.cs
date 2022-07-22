using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using WorkFlowEntry.Models;
using System.Data.SqlClient;
using System.Net.Http;
using Newtonsoft.Json;
using WorkFlowEntry.Repositories;
using WorkFlowEntry.Repository;

namespace WorkFlowEntry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkFlowController : ControllerBase
    {
        private readonly IWorkFlowService _workflowservice;
        private readonly ILogger<WorkFlowController> _logger;
        APIResponse responseData = new APIResponse();

        WorkFlowContext db = new WorkFlowContext();

        public WorkFlowController(IWorkFlowService workflowservice, ILogger<WorkFlowController> logger)
        {
            _workflowservice = workflowservice;
            _logger = logger;
           
        }

        [HttpPut]
        [Route("CreateWF")]
        
        public ActionResult<WorkFlow_Master> CreateWF([FromBody] WorkFlow_Master wf)
        {
            _workflowservice.Create(wf);
            _logger.LogInformation("Work Flow Inserted successfully");
            return Ok();
        }
        [HttpPost]
        [Route("EditWF/{id}")]
        public async Task<ActionResult<APIResponse>> EditWF([FromBody] WorkFlow_Master wf, int id)
        {
            await _workflowservice.Edit(wf, id);
            responseData.ResponseCode = (int)enumResponseCode.Success;
            responseData.ResponseMessage = "OK";
            _logger.LogInformation("Work Flow is Updated successfully");
            return responseData;
        }

        [HttpGet]
        [Route("DeleteWF/{id}")]
        public async Task<ActionResult<APIResponse>> DeleteWF(int id)
        {
            await _workflowservice.Delete(id);
            responseData.ResponseCode = (int)enumResponseCode.Success;
            responseData.ResponseMessage = "OK";
            _logger.LogInformation("Work Flow is Deleted successfully");
            return responseData;

        }
        //[HttpGet]
        //[Route("GetWLList/{id?}")]
        //public async Task<ActionResult<APIResponse>> GetWLList(int id)
        // {
        //     var data = await _workflowservice.GetWLList(id);
        //    //str = JsonConvert.SerializeObject(new { data = data});
        //    responseData.ResponseCode = (int)enumResponseCode.Success;
        //    responseData.ResponseMessage = "OK";
        //    responseData.ResponseObject = data;
        //    _logger.LogInformation("Work Flow Listed successfully");
        //    return  responseData;
        //}

        [HttpGet]
        [Route("GetWorkflowList/{id}/{ipro_id}/{ics_id}")]

        public async Task<ActionResult<APIResponse>> GetWorkflowList(int id,int ipro_id, int ics_id)
        {
            //throw new Exception("Error");
            var data = await _workflowservice.GetWorkflowList(id,ipro_id, ics_id);
            //str = JsonConvert.SerializeObject(new { data = data});
            responseData.ResponseCode = (int)enumResponseCode.Success;
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Work Flow Listed successfully");
            return responseData;
        }

        [HttpPost]
        [Route("WF_Dtl_Post/{Req_id}/{Step_id}/{User_id}")]
        public async Task<ActionResult<APIResponse>> WF_Dtl_Post(int Req_id, int Step_id, int User_id)
        {
            var data = await _workflowservice.WF_Dtl_Post(Req_id, Step_id,User_id);
            responseData.ResponseCode = (int)enumResponseCode.Success;
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Work Flow is inserted successfully");
            return responseData;
           
        }
    }
}
