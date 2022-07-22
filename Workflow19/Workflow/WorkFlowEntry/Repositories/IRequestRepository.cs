using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;

namespace WorkFlowEntry.Repositories
{
   public interface IRequestRepository
    {
        Process_Request CreateRequestProcess(Process_Request request, int processid);
        bool RequestProcessDefination(int Processid);

        Task<List<WorkFlowRoleApprovals>> WFApprovalList(int processid);
        // WorkFlowInstance CreateWFInstance(WorkFlowInstance WFInstance, int processid);
    }
}
