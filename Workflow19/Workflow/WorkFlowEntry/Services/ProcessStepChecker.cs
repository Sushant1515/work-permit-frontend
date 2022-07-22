using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Repositories;

namespace WorkFlowEntry.Services
{
    public interface IProcessStepCheck
    {
        Task<bool> ProcessStepStatus(int processid);

        //bool ProcessStepsApprivalStatus(int processid);

    }
    public class ProcessStepChecker : IProcessStepCheck
    {
        private readonly IProcessStepsRepository _processstep;

        public ProcessStepChecker(IProcessStepsRepository processstep)
        {
            _processstep = processstep;
        }

        public async Task<bool> ProcessStepStatus(int processid)
        {
            return await _processstep.ProcessStepStatus(processid);
        }
    }
}
