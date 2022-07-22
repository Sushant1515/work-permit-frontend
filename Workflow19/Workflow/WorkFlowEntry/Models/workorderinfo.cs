using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WorkFlowEntry.Models
{
    public class workorderinfo
    {
        [Key]
        public string id { get; set; }
        public string workorder { get; set; }
    }
}
