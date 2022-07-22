using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowEntry.Models;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WorkFlowEntry.Services
{
    public class DateAPIService:IDataApiService
    {
        WorkFlowContext _wpdb = new WorkFlowContext();
        HttpClient httpClient = new HttpClient();

        public async Task<List<Data_API>> DataApi()
        {
            List<Data_API> Data_Api_List = new List<Data_API>();
                using (var response = await httpClient.GetAsync(WebApiUrl.webapi))
                {
                 var responsedata=await response.Content.ReadAsStringAsync();
                 
                      var datalist = JObject.Parse(responsedata);
                    var list = datalist["data"].ToObject<List<Data_API>> ();
                    Data_Api_List = list;
                }
            
            return Data_Api_List;
        }

       

        public async Task<List<Data_API>> GetAsset()
        {
            List<Data_API> Data_Api_List = new List<Data_API>();
                using (var response = await httpClient.GetAsync(WebApiUrl.webapi))
                {
                    var responsedata = await response.Content.ReadAsStringAsync();
                    var datalist = JObject.Parse(responsedata);
                    var list = datalist["data"].ToObject<List<Data_API>>();
                    Data_Api_List = list;
                }
           
            return Data_Api_List;
        }

        public async Task<List<Data_API>> Equipment()
        {
            List<Data_API> Data_Api_List = new List<Data_API>();
                using (var response = await httpClient.GetAsync(WebApiUrl.webapi))
                {
                    var responsedata = await response.Content.ReadAsStringAsync();
                    var datalist = JObject.Parse(responsedata);
                    var list = datalist["data"].ToObject<List<Data_API>>();
                    Data_Api_List = list;
                }
            
            return Data_Api_List;
        }

        public async Task<List<Data_API>> GetLocation()
        {
            List<Data_API> Data_Api_List = new List<Data_API>();
                using (var response = await httpClient.GetAsync(WebApiUrl.webapi))
                {
                    var responsedata = await response.Content.ReadAsStringAsync();
                    var datalist = JObject.Parse(responsedata);
                    var list = datalist["data"].ToObject<List<Data_API>>();
                    Data_Api_List = list;
                }
          
            return Data_Api_List;
        }

        //public async Task<List<CurrentPrice>> CurrentPriceApi()
        //{
        //    List<CurrentPrice> CurrentPriceList = new List<CurrentPrice>();
        //    using (var httpClient = new HttpClient())
        //    {
        //       // dynamic obj = new Object();
        //        using (var response = await httpClient.GetAsync("https://api.coindesk.com/v1/bpi/currentprice.json"))
        //        {
        //            var responsedata = await response.Content.ReadAsStringAsync();
        //            var datalist = JObject.Parse(responsedata);
        //            var list = datalist["data"].ToObject<List<CurrentPrice>>();
        //            CurrentPriceList = list;

        //        }
        //    }
        //    return CurrentPriceList;
        //}
    }
}
