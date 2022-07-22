using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Net;
using System.Net.Mail;

using WorkFlowEntry.Models;
using WorkFlowEntry.Services;

using FluentEmail.Core;

using System.Xml;

using Microsoft.AspNetCore.Hosting;



namespace WorkFlowEntry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailSenderController : ControllerBase
    {
       
        private readonly IMailSenderService _mailSender;
     

        public MailSenderController(IMailSenderService mailSender)
        {
            _mailSender = mailSender;
        }

       

      

        [HttpPost]
        [Route("sendmail")]
       
       public  IActionResult sendmail([FromServices] IFluentEmail mailer)
        {


            Person person = new Person() { Name = "Nitin", Address = "Pune", Code = "BVUPK1567J" };
          _mailSender.SendHtmlGmail("ashu2011007@gmail.com", "Ashu", person);
            return Ok();
        }


      



    }
}
