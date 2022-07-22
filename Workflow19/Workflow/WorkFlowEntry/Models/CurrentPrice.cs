using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Models
{
    public class CurrentPrice
    {
        public string code { get; set; }
        public string symbol { get; set; }
        public decimal rate { get; set; }
        public string description { get; set; }
        public decimal rate_float { get; set; }
    }
}
