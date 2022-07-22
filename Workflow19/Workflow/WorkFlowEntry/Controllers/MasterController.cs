using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkFlowEntry.Models;
using WorkFlowEntry.Services;
using Microsoft.Extensions.Logging;

namespace WorkFlowEntry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private IMasterService _IMasterService;
        private readonly ILogger<MasterController> _logger;
        APIResponse responseData = new APIResponse();

        public MasterController(IMasterService IMaster, ILogger<MasterController> logger)
        {
            _IMasterService = IMaster;
            _logger = logger;

        }
        #region Asset
        [HttpGet]
        [Route("assetlist")]
        public async Task<ActionResult<APIResponse>> AssetList()
        {          
            var data = await _IMasterService.AssetList();
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Asset master listed successfully");
            return responseData;
        }
        #endregion

        #region Type
        [HttpGet]
        [Route("typelist")]
        public async Task<ActionResult<APIResponse>> TypeList()
        {
            var data = await _IMasterService.TypeList();
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Type master listed successfully");
            return responseData;
        }
        #endregion

        #region Subtype
        [HttpGet]
        [Route("stypelist")]
        public async Task<ActionResult<APIResponse>> StypeList()
        {
            var data = await _IMasterService.StypeList();
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Subtype master listed successfully");
            return responseData;
        }
        #endregion

        #region Area
        [HttpGet]
        [Route("arealist")]
        public async Task<ActionResult<APIResponse>> AreaList()
        {
            var data = await _IMasterService.AreaList();
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Area master listed successfully");
            return responseData;
        }
        #endregion

        #region Applicant
        [HttpGet]
        [Route("appplicantlist")]
        public async Task<ActionResult<APIResponse>> ApplicantList()
        {
            var data = await _IMasterService.ApplicantList();
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Applicant master listed successfully");
            return responseData;
        }
        #endregion

        #region WorkOrder
        [HttpGet]
        [Route("workorderlist")]
        public async Task<ActionResult<APIResponse>> WorkOrderList()
        {
            var data = await _IMasterService.WorkOrderList();
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Workorder master listed successfully");
            return responseData;
        }
        #endregion
    }
}
