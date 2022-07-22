using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace WorkFlowEntry.Services
{
    public interface IIncidentMgmtService
    {
        Task<List<tbl_incidentmgmt>> IncidentList();
        Task<tbl_incidentmgmt> IncidentFetch(string id);
        Task<tbl_incidentmgmt> IncidentSave(tbl_incidentmgmt incidentmgmt);
        Task<tbl_incidentmgmt> IncidentUpdate(tbl_incidentmgmt incidentmgmt, string id);
    }
}
