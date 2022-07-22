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
    public class DataAPIController : ControllerBase
    {
        private IDataApiService _IData_ApiService;
        private readonly ILogger<DataAPIController> _logger;
        APIResponse responseData = new APIResponse();

        public DataAPIController(IDataApiService IData_ApiService, ILogger<DataAPIController> logger)
        {
            _IData_ApiService = IData_ApiService;
            _logger = logger;

        }

        [HttpGet]
        [Route("DataApi")]
        public async Task<ActionResult<APIResponse>> DataApi()
        {         
            var data = await _IData_ApiService.DataApi();           
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Work Flow Listed successfully");
            return responseData;
        }

        [HttpGet]
        [Route("GetAsset")]
        public async Task<ActionResult<APIResponse>> GetAsset()
        {
            var data = await _IData_ApiService.GetAsset();
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Work Flow Listed successfully");
            return responseData;
        }

        [HttpGet]
        [Route("Equipment")]
        public async Task<ActionResult<APIResponse>> Equipment()
        {
            var data = await _IData_ApiService.Equipment();
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Work Flow Listed successfully");
            return responseData;
        }

        [HttpGet]
        [Route("GetLocation")]
        public async Task<ActionResult<APIResponse>> GetLocation()
        {
            var data = await _IData_ApiService.GetLocation();
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = data;
            _logger.LogInformation("Work Flow Listed successfully");
            return responseData;
        }


        //[HttpGet]
        //[Route("CurrentPriceApi")]
        //public async Task<ActionResult<APIResponse>> CurrentPriceApi()
        //{

        //    var data = await _IData_ApiService.CurrentPriceApi();


        //    responseData.ResponseMessage = "OK";
        //    responseData.ResponseObject = data;
        //    _logger.LogInformation("Work Flow Listed successfully");
        //    return responseData;
        //}
    }
}
