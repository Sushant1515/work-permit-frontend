using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using RuleEngineService.Models;
using RulesEngine.Extensions;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowEntry.Models;


namespace RuleEngineService.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        APIResponse responseData = new APIResponse();

        [HttpGet]
        [Route("api/Stage/StageChecker")]
        public async Task<ActionResult<APIResponse>> StageChecker()
        {

            // BasicInfo bs = new BasicInfo();
            //BasicInfo1 bs1 = new BasicInfo1();
            ////input data here
            //var basicInfo = "{\"country2\": \"india2\",\"country\": \"india\",\"country1\": \"india1\"}";
            //var basicInfo1 = "{\"orders\": \"india1\",\"items\": \"india\",\"project\": \"india1\"}";
            //bs.country2 = "india2";
            //bs.country = "india";
            //bs.country1 = "india1";
            //bs1.orders = "india1";
            //bs1.items = "india";
            //bs1.project = "india1";

            //input data here from models
            BasicInfo bs = new BasicInfo();
            bs.country = "india123";
            bs.state = "rajasthan";
            bs.picode = "313001";
            bs.cityname = "udaipur";

            OrdeInfo order = new OrdeInfo();

            order.ordername = "credit";
            order.qty = 15;
            order.amount = 2000;

            var rp1 = new RuleParameter("basicInfo",bs);
            var rp2 = new RuleParameter("Orderinfo", order);
            string discountOffered = "No discount offered.";

            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "WorkflowRules.json", SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
                responseData.ResponseMessage = "Rules not found";

            var jsonData = System.IO.File.ReadAllText(files[0]);
            var workflowrules = JsonConvert.DeserializeObject<List<RulesEngine.Models.Workflow>>(jsonData);
            var re = new RulesEngine.RulesEngine(workflowrules.ToArray(), null);

            var resultList = await re.ExecuteAllRulesAsync("DiscountWithCustomInputNames", rp1,rp2);
              

            //foreach (var result in resultList)
            //{
            //    Console.WriteLine($"Rule - {result.Rule.RuleName}, IsSuccess - {result.IsSuccess}");
            //    //responseData.ResponseMessage = "Rule - " + result.Rule.RuleName + ", IsSuccess - " + result.IsSuccess +"";
            //}

            

            resultList.OnSuccess((eventName) => {
                //do write logic here
                responseData.ResponseMessage = eventName;
                discountOffered = $"Discount offered is {eventName} % over MRP.";
            });
            resultList.OnFail(() => {
                //do write logi here  
                responseData.ResponseMessage = "No Condition Match";
                discountOffered = "The user is not eligible for any discount.";
            });

            responseData.ResponseCode = (int)enumResponseCode.Success;
            return responseData;


        }


    }
}
