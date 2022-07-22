using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using WorkFlowEntry.Models;
using System.Runtime.InteropServices;
using WorkFlowEntry.Repositories;

namespace WorkFlowEntry.Repository
{
    public interface IWorkFlowService
    {
        Task<List<WorkFlow_Master>> GetWLList(int id);
        Task<List<WorkFlow_Instance>> GetWorkflowList(int id,int ipro_id, int ics_id);
        Task<string> WF_Dtl_Post(int Req_id, int Step_id, int User_id);
        
        Task<bool> Edit(WorkFlow_Master workflow,int id);
        Task<bool> Delete(int id);
        WorkFlow_Master Create(WorkFlow_Master workflow);

    }
    public class WorkFlowService : IWorkFlowService
    {
        private readonly IWorkflowRepository _workFlowRepository;
        public WorkFlowService(IWorkflowRepository workFlowRepository)
        {
            _workFlowRepository = workFlowRepository;
        }

        public WorkFlow_Master Create(WorkFlow_Master workflow)
        {
          return  _workFlowRepository.Create(workflow);
        }

        public Task<bool> Delete(int id)
        {
            return _workFlowRepository.Delete(id);
        }
        public Task<bool> Edit(WorkFlow_Master workflow, int id)
        {
            return _workFlowRepository.Edit(workflow, id);
        }
        public Task<List<WorkFlow_Master>> GetWLList(int id)
        {
            return _workFlowRepository.GetWLList(id);
        }

        public Task<List<WorkFlow_Instance>> GetWorkflowList(int id,int ipro_id, int ics_id)
        {
          
            return _workFlowRepository.GetWorkflowList(id, ipro_id, ics_id);
        }
        
            public Task<string> WF_Dtl_Post(int Req_id,int Step_id,int User_id)
        {

            return _workFlowRepository.WF_Dtl_Post(Req_id,Step_id,User_id);
        }
    }
}
