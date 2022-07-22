using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace WorkFlowEntry.Services
{
   public interface IWorkPermitService
    {
        Task<List<WorkPermitRequest>> workpermitlist(string userid);
        Task<WorkPermitRequest> workpermitget(string id);
      Task < WorkPermitRequest> workpermitcreate(WorkPermitRequest WorkPermit);
       Task <WorkPermitRequest> wpedit(WorkPermitRequest WorkPermit,string id);
        Task<workpermitconfig> wpcsave(workpermitconfig WorkPermitconfig);
        Task<tbl_image> imgsave(tbl_image image);

         string GetRandomNumber();




    }
}
