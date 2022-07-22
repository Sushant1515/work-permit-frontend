using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;

namespace WorkFlowEntry.Services
{
    public interface IMailSenderService
    {

        void SendHtmlGmail(string recipientEmail, string recipientName, Person person);

       
    }
}
