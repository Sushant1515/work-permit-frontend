using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowEntry.Models
{
    public class WorkFlow_Process_Mapping
    {
        [ForeignKey("WorkFlow_Master")]
        public int iWF_Id { get; set; }
        [Key]
        public int iProcess_Id { get; set; }
        public DateTime dCreated_Date { get; set; }
        public int iCreated_User_Cd { get; set; }

    }
}
