using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowRequestServices.Models
{
    public class WorkFlowInstanceDetails
    {
        [Key]
        public int iWF_Instance_Id { get; set; }
        public int iStep_Id { get; set; }
        public int iApproval_Role_Id { get; set; }
        public int iApproval_User_Cd { get; set; }
        public int iStatus_Id { get; set; }
    }
}
