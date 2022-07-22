using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;

namespace WorkFlowEntry.Services
{
   public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest mailRequest);
            
    }
}
