using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Models
{
    public class WorkFlow_Step_Transition_Master
    {
        public int iTransition_Id { get; set; }
        public int iWF_Id { get; set; }
        public int iCurrent_Step_Id { get; set; }
        public int iNext_Step_Id { get; set; }
        public string sCondition { get; set; }
    }
}
