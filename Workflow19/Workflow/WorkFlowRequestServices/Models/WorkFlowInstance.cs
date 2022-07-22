using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowRequestServices.Models
{
    public class WorkFlowInstance
    {
        [Key]
        public int iWF_Instance_Id { get; set; }
        public int iWF_Id { get; set; }
        public int iProcess_Id { get; set; }
        public int iRequest_Id { get; set; }
        public int iCurrent_Step_Id { get; set; }
        public int iNext_Step_Id { get; set; }
        public int iCurrent_Step_Approval_Role_Id { get; set; }
        public int iStatus_Id { get; set; }
    }
}
