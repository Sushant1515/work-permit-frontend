using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Repositories
{
   public interface IProcessStepsRepository
    {
        Task<bool> ProcessStepStatus(int processid);

       // bool ProcessStepsApprivalStatus(int processid);
    }
}
