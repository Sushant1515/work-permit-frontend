using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkFlowEntry.Models;
using WorkFlowEntry.Services;

namespace WorkFlowEntry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentMgmtController : ControllerBase
    {
        private IIncidentMgmtService _IIncidentMgmtService;
        private readonly ILogger<IncidentMgmtController> _logger;
        APIResponse responseData = new APIResponse();

        public IncidentMgmtController(IIncidentMgmtService IincedentMgmt, ILogger<IncidentMgmtController> logger)
        {
            _IIncidentMgmtService = IincedentMgmt;
            _logger = logger;
            
        }

        #region IncidentMgmt
        [HttpGet]
        [Route("incList")]
        public async Task<ActionResult<APIResponse>> IncidentList()
        {
            var data = await _IIncidentMgmtService.IncidentList();
            responseData.ResponseCode = (int)enumResponseCode.Success;
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Incident Listed successfully");
            return responseData;
        }
        [HttpGet]
        [Route("incfetch/{id}")]
        public async Task<ActionResult<APIResponse>> IncidentFetch(string id)
        {
            var incidentObj = await _IIncidentMgmtService.IncidentFetch(id);
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = incidentObj;
            _logger.LogInformation("Incident Listed successfully");
            return responseData;
        }
        [HttpPost]
        [Route("incsave")]
        public async Task<ActionResult<APIResponse>> IncidentSave([FromBody] tbl_incidentmgmt incidentmgmt)
        {
            await _IIncidentMgmtService.IncidentSave(incidentmgmt);
            _logger.LogInformation("Incident Inserted successfully");
            return Ok();
        }
        [HttpPost]
        [Route("incupdate/{id}")]
        public async Task<ActionResult<APIResponse>> IncidentUpdate([FromBody] tbl_incidentmgmt incidentmgmt, string id)
        {
            await _IIncidentMgmtService.IncidentUpdate(incidentmgmt, id);
            responseData.ResponseCode = (int)enumResponseCode.Success;
            responseData.ResponseMessage = "OK";
            _logger.LogInformation("Incident is Updated successfully");
            return responseData;
        }
        #endregion
    }
}
