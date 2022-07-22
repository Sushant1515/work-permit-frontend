using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowRequestServices.Models;
namespace WorkFlowRequestServices.Interface
{
    interface IWorkFlowProcess
    {
        int Generaterequest(ProcessRequest request,int processid);
        Task<int> CreateWorkFlowProcess(WorkFlowProcessMaster workflowProcess);

        Task<int> CreateWorkFlowProcessMapping(int WFId, WorkFlowProcessMaster workflowProcess,
                                           WorkFlowProcessMapping workFlowMapping,
                                           WorkFlowInstance workFlowInstance);



    }
}
