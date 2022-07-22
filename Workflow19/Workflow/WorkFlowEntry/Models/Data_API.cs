using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WorkFlowEntry.Models
{
    public class Data_API
    {
        
       
        [JsonProperty(PropertyName = "ID Nation")]
        public string ID_Nation { get; set; }
        [JsonProperty(PropertyName = "Nation")]
        public string Nation { get; set; }
        [JsonProperty(PropertyName = "United States")]
        public string United_States { get; set; }
        [JsonProperty(PropertyName = "ID Year")]
        public int ID_Year { get; set; }
        [JsonProperty(PropertyName = "Year")]
        public int Year { get; set; }
        [JsonProperty(PropertyName = "Population")]
        public int Population { get; set; }
        [JsonProperty(PropertyName = "Slug Nation")]
        public string Slug_Nation {get;set;}
    }

    public class Data_APiList
    {
        public List<Data_API> DataList { get; set; }
    }
}
