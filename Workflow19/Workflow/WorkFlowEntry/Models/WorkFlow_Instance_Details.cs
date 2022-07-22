using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlowEntry.Models
{
    public class WorkFlow_Instance_Details
    {
        [Key]
        public decimal iTran_no { get; set; }
        public decimal iWF_Instance_Id { get; set; }
        public int iStep_Id { get; set; }
        public Int16 iApproval_Role_Id { get; set; }
        public decimal iApproval_User_Cd { get; set; }
        public int iStatus_Id { get; set; }

        public DateTime dCreated_Date { get; set; }
        public DateTime dUpdated_Date { get; set; }
    }
}
