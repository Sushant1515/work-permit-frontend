using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using WorkFlowEntry.Services;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Microsoft.AspNetCore.Cors;

namespace WorkFlowEntry.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class WorkPermitController : ControllerBase
    {
        private IWorkPermitService _IWorkPermitService;
        private readonly ILogger<WorkPermitController> _logger;
        private IHostingEnvironment Environment;
        APIResponse responseData = new APIResponse();
        WorkFlowContext _wpdb = new WorkFlowContext();
        private string Path = null;

        public WorkPermitController(IWorkPermitService IworkPermit, ILogger<WorkPermitController> logger, IHostingEnvironment _environment)
        {
            _IWorkPermitService = IworkPermit;
            _logger = logger;

            this.Environment = _environment;
            Path = _environment.WebRootPath + "\\Webimg\\";
        }
        #region WorkPermit
        [HttpGet]
        [Route("workpermitlist/{userid}")]
        public async Task<ActionResult<APIResponse>> workpermitlist(string userid)
        {
            
            var workpermitObj = await _IWorkPermitService.workpermitlist(userid);           
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = workpermitObj;         
            _logger.LogInformation("Work Permit Listed successfully");
            return responseData;
        }

        [HttpGet]
        [Route("workpermitget/{id}")]
        public async Task<ActionResult<APIResponse>> workpermitget(string id)
        {
            var workpermitObj = await _IWorkPermitService.workpermitget(id);
            responseData.ResponseMessage = "OK";
            responseData.ResponseObject = workpermitObj;
            _logger.LogInformation("Work Permit Listed successfully");
            return responseData;
        }
        [HttpPost]
        [Route("workpermitcreate")]
        public async Task<ActionResult<APIResponse>> workpermitcreate([FromBody] WorkPermitRequest WorkPermit)
        {
             await _IWorkPermitService.workpermitcreate(WorkPermit);
            _logger.LogInformation("Work Permit Inserted successfully ");
            return Ok();
        }
        [HttpPost]
        [Route("wpedit/{id}")]
        public async Task<ActionResult<APIResponse>> wpedit([FromBody] WorkPermitRequest WorkPermit, string id)
        {
            await _IWorkPermitService.wpedit(WorkPermit, id);
            responseData.ResponseCode = (int)enumResponseCode.Success;
            responseData.ResponseMessage = "OK";
            _logger.LogInformation("Work Permit is Updated successfully");
            return responseData;
        }
        #endregion
        #region WorkPermitConfig
        [HttpPost]
        [Route("wpcsave")]
        
        public async Task<ActionResult<APIResponse>> wpcsave([FromBody] workpermitconfig WorkPermitconfig)
        {
            await _IWorkPermitService.wpcsave(WorkPermitconfig);
            _logger.LogInformation("Work Permit Configuration Inserted Successfully ");
            return Ok();
        }
        #endregion

        #region ImageUpload&Save
        [HttpPost]
        [Route("UploadDocument")]
        public async Task< ActionResult<APIResponse>> UploadDocument()
        {
            int counts = _wpdb.tbl_image.Count();
            counts = counts + 1;
            IFormFile file = Request.Form.Files[0];
            string[] FileType = { "image/jpeg", "image/png", "image/jpg" };

            if(!FileType.Contains(file.ContentType))
            {
                responseData.ResponseMessage = "Invalid File Type only jpeg/png format allowed";
                return StatusCode(500, responseData);
            }
            var randomFileName = "Img" + counts.ToString() + ""+file.FileName+"";

            using (var stream=new FileStream(Path+ randomFileName,FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            responseData.ResponseMessage = "Success";
            responseData.ResponseObject = new { filename = randomFileName, Path = Path + randomFileName };
            tbl_image image = new tbl_image();
            image.id = _IWorkPermitService.GetRandomNumber();
            image.imgdate = System.DateTime.Now;
            image.imgname = randomFileName;
            image.imgpath = Path + randomFileName;
            await _IWorkPermitService.imgsave(image);
            return  StatusCode(200, responseData);
        }

        #endregion

    }


}

