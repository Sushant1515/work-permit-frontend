using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;


namespace WorkFlowEntry.Services
{
    public interface IMasterService
    {
        Task<List<assetinfo>> AssetList();
        Task<List<typeinfo>> TypeList();
        Task<List<subtypeinfo>> StypeList();
        Task<List<areainfo>> AreaList();
        Task<List<applicantinfo>> ApplicantList();
        Task<List<workorderinfo>> WorkOrderList();
       
    }
}
