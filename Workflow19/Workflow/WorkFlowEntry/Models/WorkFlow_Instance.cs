using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Models
{
    public class WorkFlow_Instance
    {
        [Key]
        public decimal iWF_Instance_Id { get; set; }
        public decimal iWF_Id { get; set; }
        public int iProcess_Id { get; set; }
        public int iRequest_Id { get; set; }
        public int iCurrent_Step_Id { get; set; }
        public int iNext_Step_Id { get; set; }
        public int iCurrent_Step_Approval_Role_Id { get; set; }
        public int iStatus_Id { get; set; }

        public DateTime dCreated_Date { get; set; }
        public DateTime dUpdated_Date { get; set; }


    }
}
