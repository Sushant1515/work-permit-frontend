using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using WorkFlowEntry.Repositories;

namespace WorkFlowEntry.Services
{
    public interface ICreaterequestservice
    {
        Process_Request CreateRequestProcess(Process_Request request,int processid);

        bool RequestProcessDefination(int Processid);

        Task<List<WorkFlowRoleApprovals>> WFApprovalList(int processid);

        

    }

    public class ProcessRequest : ICreaterequestservice
    {
        private readonly IRequestRepository _requestrepository;
        public ProcessRequest(IRequestRepository requestrepository)
        {
            _requestrepository = requestrepository;
        }
        public Process_Request CreateRequestProcess(Process_Request request, int processid)
        {
            return _requestrepository.CreateRequestProcess(request, processid);
        }

        public bool RequestProcessDefination(int Processid)
        {
            return _requestrepository.RequestProcessDefination(Processid);
        }

        public Task<List<WorkFlowRoleApprovals>> WFApprovalList(int processid)
        {
            return _requestrepository.WFApprovalList(processid);
        }


    }
}
