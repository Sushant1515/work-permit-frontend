using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Models
{
    public class Process_Request
    {
        [Key]
        public int iRequest_Id { get; set; }
        public int iStatus_Id { get; set; }
        public DateTime dCreated_Date { get; set; }
        public DateTime dUpdated_Date { get; set; }
    }

    public class RequestObject
    {
        public string country { get; set; }
        public string state { get; set; }
        public string cityname { get; set; }
    }

}
