using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;

namespace WorkFlowEntry.Repositories
{
   public interface IWorkflowRepository
    {
        Task<List<WorkFlow_Master>> GetWLList(int id);
        Task<List<WorkFlow_Instance>> GetWorkflowList(int id,int ipro_id, int ics_id);
        Task<bool> Edit(WorkFlow_Master workflow,int id);
        Task<bool> Delete(int id);
        Task<string> WF_Dtl_Post(int Req_id, int Step_id, int User_id);
        WorkFlow_Master Create(WorkFlow_Master workflow);
    }


}
