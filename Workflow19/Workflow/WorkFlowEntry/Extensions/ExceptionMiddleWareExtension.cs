using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WorkFlowEntry.Models;

namespace WorkFlowEntry.Extensions
{
    
    public static class ExceptionMiddleWareExtension
    {
       
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
              
        app.UseExceptionHandler(
                appError =>
                {
                    appError.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "application/json";
                            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                            if (null != contextFeature)
                            {
                                var errorMessage = $"<b>Exception Error: {contextFeature.Error.Message} </b> {contextFeature.Error.StackTrace}";
                            
                                var errorMess = $"<b>StatusCode: {context.Response.StatusCode} </b> Message: {contextFeature.Error.Message}";
                                var path = Directory.GetCurrentDirectory() + "\\Logs\\";
                                string filename ="LogError-"+ DateTime.Now.ToString("ddMMyyyyHHmmss",CultureInfo.InvariantCulture)+ ".txt";
                                // loggerFactory.AddFile($"{path}\\Logs\\Log.txt");
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }

                                File.WriteAllText(Path.Combine(path, filename), errorMess);
                                 
                                //await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                                await context.Response.WriteAsync(new ErrorDetails
                                {
                                    StatusCode = context.Response.StatusCode,
                                    //Message = "Internal Server Error"
                                    Message = contextFeature.Error.Message


                                }.ToString());

                               
                            }
                        });
                }
            );

        }

    }
}
