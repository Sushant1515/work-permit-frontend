using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using WorkFlowEntry.Services;

namespace WorkFlowEntry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessStepController : ControllerBase
    {

        private readonly ILogger<ProcessStepController> _logger;
        private readonly IProcessStepCheck _processstepcheck;

        APIResponse responseData = new APIResponse();

        public ProcessStepController(IProcessStepCheck processStepCheck, ILogger<ProcessStepController> logger)
        {
            _logger = logger;
            _processstepcheck = processStepCheck;
        }

        [HttpPut]
        [Route("ProcessStepMove/{id}")]
        public ActionResult<APIResponse> ProcessStepMove(int id)
        {
            _processstepcheck.ProcessStepStatus(id);
            responseData.ResponseCode = (int)enumResponseCode.Success;
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = "";
            return responseData;
        }


    }
}
