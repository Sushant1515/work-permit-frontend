using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Models
{
    public class WF_Approval_Escalation_Defination
    {
        [Key]
        public int iTran_id { get; set; }
        public int iWf_Id { get; set; }
        public int iStep_Id { get; set; }
        public int iDuration_Start { get; set; }
        public int iDuration_End { get; set; }
        public int iEscalate_Role_Id { get; set; }
        public int iNext_Escalate_Role_Id { get; set; }
        public string sNext_Escalte_EmailId { get; set; }

    }
}
