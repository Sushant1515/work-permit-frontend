using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace WorkFlowEntry.Services
{
   
    public class MasterService:IMasterService
    {
        WorkFlowContext _wpdb = new WorkFlowContext();

        public static string GetRandomNumber()
        {
            Guid obj = Guid.NewGuid();
            return obj.ToString();
        }
        #region Asset
        public async Task<List<assetinfo>> AssetList()
        {
            return await _wpdb.assetinfo.ToListAsync();
        }
        #endregion

        #region Type
        public async Task<List<typeinfo>> TypeList()
        {
            return await _wpdb.typeinfo.ToListAsync();
        }
        #endregion

        #region Subtype
        public async Task<List<subtypeinfo>> StypeList()
        {
            return await _wpdb.subtypeinfo.ToListAsync();
        }
        #endregion

        #region Area
        public async Task<List<areainfo>> AreaList()
        {
            return await _wpdb.areainfo.ToListAsync();
        }
        #endregion

        #region Applicant
        public async Task<List<applicantinfo>> ApplicantList()
        {
            return await _wpdb.applicantinfo.ToListAsync();
        }
        #endregion

        #region WorkOrder
        public async Task<List<workorderinfo>> WorkOrderList()
        {
            return await _wpdb.workorderinfo.ToListAsync();
        }
        #endregion

    }
}
