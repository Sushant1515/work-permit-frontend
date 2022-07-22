using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using WorkFlowEntry.Repositories;
using WorkFlowEntry.Services;

namespace WorkFlowEntry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestProcessController : ControllerBase
    {
        private readonly ILogger<RequestProcessController> _logger;
        private readonly ICreaterequestservice _requestservice;

        APIResponse responseData = new APIResponse();

        public RequestProcessController(ICreaterequestservice requestservice, ILogger<RequestProcessController> logger)
        {
            _requestservice = requestservice;
            _logger = logger;
        }

        [HttpPut]
        [Route("CreateRequestProcess/{pid}")]
        public ActionResult<Process_Request> CreateRequestProcess([FromBody] Process_Request request,int pid)
        {
            //bool isDefinationDefined = _requestservice.RequestProcessDefination(pid);
            bool isDefinationDefined = true;
            if (isDefinationDefined == true)
            {
                var data = _requestservice.CreateRequestProcess(request, pid);
                _logger.LogInformation("Work Flow Request and Instance Inserted successfully");
            }
            else
            {
                _logger.LogInformation("Work Flow Process Defination Not Defined");
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetWLApprovalList/{id}")]
        public async Task<ActionResult<APIResponse>> GetWLApprovalList(int id)
        {
            var data = await _requestservice.WFApprovalList(id);
            responseData.ResponseCode = (int)enumResponseCode.Success;
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            return responseData;
        }

       


    }
}
