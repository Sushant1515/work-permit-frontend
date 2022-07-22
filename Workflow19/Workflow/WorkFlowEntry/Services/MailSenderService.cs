using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using FluentEmail.Core;
using Microsoft.Extensions.DependencyInjection;
using System.IO;


namespace WorkFlowEntry.Services
{
    public class MailSenderService:IMailSenderService
    {
        private readonly IServiceProvider _serviceProvider;

        


        public MailSenderService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async void SendHtmlGmail(string recipientEmail, string recipientName, Person person)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                var email = mailer
                    .To(recipientEmail, recipientName)
                    .Subject("Hello there HTML")
                    .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                    person);

                await email.SendAsync();
            }
        }

       
    }
}
