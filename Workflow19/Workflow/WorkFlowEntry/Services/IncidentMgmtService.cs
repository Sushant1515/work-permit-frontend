using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace WorkFlowEntry.Services
{
    public class IncidentMgmtService:IIncidentMgmtService
    {
        WorkFlowContext _wpdb = new WorkFlowContext();

        public static string GetRandomNumber()
        {
            Guid obj = Guid.NewGuid();
            return obj.ToString();
        }

        #region IncidentMgmt
        public async Task<List<tbl_incidentmgmt>> IncidentList()
        {
            return await _wpdb.tbl_incidentmgmt.ToListAsync();
        }
        public async Task<tbl_incidentmgmt> IncidentFetch(string id)
        {
            var incdetail = await _wpdb.tbl_incidentmgmt.FirstOrDefaultAsync(x => x.id == id);
            return incdetail;
        }
        public async Task<tbl_incidentmgmt> IncidentSave(tbl_incidentmgmt incidentmgmt)
        {
            incidentmgmt.id = GetRandomNumber();
            _wpdb.tbl_incidentmgmt.Add(incidentmgmt);
            await _wpdb.SaveChangesAsync();
            return incidentmgmt;
        }
        public async Task<tbl_incidentmgmt> IncidentUpdate(tbl_incidentmgmt incidentmgmt, string id)
        {
            incidentmgmt.id = id;
            _wpdb.Entry(incidentmgmt).State = EntityState.Modified;
            await _wpdb.SaveChangesAsync();
            return incidentmgmt;
        }
        #endregion
    }
}
