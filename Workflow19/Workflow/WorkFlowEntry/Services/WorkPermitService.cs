using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using WorkFlowEntry.Models;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Xml;

using Microsoft.AspNetCore.Hosting;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WorkFlowEntry.Services
{
    
    public class WorkPermitService: IWorkPermitService
    {
        WorkFlowContext _wpdb = new WorkFlowContext();
        private IWorkPermitService _IWorkPermitService;
        private readonly ILogger<WorkPermitService> _logger;
        private IHostingEnvironment Environment;
        APIResponse responseData;


        public WorkPermitService(ILogger<WorkPermitService> logger, IHostingEnvironment _environment)
        {
           
            _logger = logger;
            this.Environment = _environment;
            responseData = new APIResponse();
        }

       
        public  string GetRandomNumber()
        {
            Guid obj = Guid.NewGuid();
            return obj.ToString();
        }
        #region WorkPermit
        public async Task<List<WorkPermitRequest>> workpermitlist(string userid)
        {
            return await _wpdb.WorkPermitRequest.Where(x=>x.createby==userid).ToListAsync();
        }
        public async Task<WorkPermitRequest> workpermitget(string id)
        {
            var WP_Detail = await _wpdb.WorkPermitRequest.FirstOrDefaultAsync(x => x.code == id);
            return  WP_Detail;           
        }
        public async Task<WorkPermitRequest> workpermitcreate(WorkPermitRequest WorkPermit)
        {            
            WorkPermit.id = GetRandomNumber();
            WorkPermit.code = "WP-" + DateTime.Now.Date.ToString("dd-MM-yyyy") +"-"+ WorkPermit.id;
           _wpdb.WorkPermitRequest.Add(WorkPermit);
            await _wpdb.SaveChangesAsync();
           
            return  WorkPermit;
        }
        public async Task<WorkPermitRequest> wpedit(WorkPermitRequest WorkPermit, string id)
        {
            WorkPermit.id = id;
            _wpdb.Entry(WorkPermit).State = EntityState.Modified;
            await _wpdb.SaveChangesAsync();
            return WorkPermit;
        }
        #endregion

        #region WorkPermitConfig
        public async Task<workpermitconfig> wpcsave(workpermitconfig WorkPermitconfig)
        {
            WorkPermitconfig.id = GetRandomNumber();

            _wpdb.workpermitconfig.Add(WorkPermitconfig);
            await _wpdb.SaveChangesAsync();
            return WorkPermitconfig;
        }
        #endregion

        #region ImageUpload&Save

        public async Task<tbl_image> imgsave(tbl_image image)
        {
           // image.id = GetRandomNumber();
            _wpdb.tbl_image.Add(image);
            await _wpdb.SaveChangesAsync();

            return image;
        }
        #endregion



    }
}
