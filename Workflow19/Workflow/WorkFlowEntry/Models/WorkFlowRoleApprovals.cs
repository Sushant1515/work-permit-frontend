using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Models
{
    public class WorkFlowRoleApprovals
    {
        public int iWF_Id { get; set; }
        public int iStep_Id { get; set; }
        public int iRole_id { get; set; }
        public string sRole_Desc { get; set; }

        public int iTransition_Id { get; set; }

    }
}
