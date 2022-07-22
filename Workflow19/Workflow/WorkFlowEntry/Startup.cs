using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Extensions;
using WorkFlowEntry.Models;
using WorkFlowEntry.Repositories;
using WorkFlowEntry.Repository;
using WorkFlowEntry.Services;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace WorkFlowEntry
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //---Gmail
            var from = Configuration.GetSection("Email")["From"];

            var gmailSender = Configuration.GetSection("Gmail")["Sender"];
            var gmailPassword = Configuration.GetSection("Gmail")["Password"];
            var gmailPort = Convert.ToInt32(Configuration.GetSection("Gmail")["Port"]);

            //---Sendgrid
            var sendGridSender = Configuration.GetSection("Sendgrid")["Sender"];
            var sendGridKey = Configuration.GetSection("Sendgrid")["SendgridKey"];

            //--Uncomment to use Gmail
            services
                .AddFluentEmail(gmailSender, from)
                .AddRazorRenderer()
                 //.AddSmtpSender(new SmtpClient("smtp.elasticemail.com")
                 .AddSmtpSender(new SmtpClient("smtp.elasticemail.com")
                
                {
                    UseDefaultCredentials = false,
                    Port = gmailPort,
                    Credentials = new NetworkCredential(gmailSender, gmailPassword),
                    EnableSsl = true,
                });
            services.Configure<EmailSetting>(Configuration.GetSection("EMailSetting"));
            services.AddTransient<IEmailService, Services.EmailService>();


            services.AddCors(o => o.AddPolicy("MyPolicy", corsBuilder =>
            {
                corsBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.AllowCredentials();
            }));
            services.AddScoped<IMailSenderService, MailSenderService>();
            services.AddScoped<IWorkPermitService, WorkPermitService>();
            services.AddScoped<IIncidentMgmtService,IncidentMgmtService>();
            services.AddScoped<IWorkFlowService, WorkFlowService>();
            services.AddScoped<IDataApiService, DateAPIService>();

            services.AddScoped<IWorkflowRepository, WorkflowRepository>();
            services.AddSingleton<IDatabasesetting, DatabaseService>();
            services.AddSingleton<IDatabaseRepository, DBRepository> ();
            services.AddScoped<ICreaterequestservice,ProcessRequest>();
            services.AddScoped<IRequestRepository, RequestRepository>();

            services.AddScoped<IProcessStepCheck, ProcessStepChecker>();
            services.AddScoped<IProcessStepsRepository, ProcessStepsRepository>();
            services.AddScoped<IMasterService, MasterService>();
            //services.AddHttpClient<IProcessStepsRepository, ProcessStepsRepository>();


            //services.AddSingleton<IDB, DBRepository>();
            //services.AddSingleton<IMessageProducer, RabbitMQProducer>();




            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
        
            // app.UseExceptionHandler("/Home/Error");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
           

            app.ConfigureExceptionHandler();
            

           // app.UseMiddleware<EncryptionMiddleware>();
           
            app.UseHttpsRedirection();
           
            
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("MyPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
