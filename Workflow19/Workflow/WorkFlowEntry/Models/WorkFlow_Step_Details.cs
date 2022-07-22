using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Models
{
    public class WorkFlow_Step_Details
    {
        [ForeignKey("WorkFlow_Master")]
        public int iWF_Id { get; set; }
        [Key]
        public int iStep_Id { get; set; }
        public int iStep_Sequence { get; set; }
    }
}
