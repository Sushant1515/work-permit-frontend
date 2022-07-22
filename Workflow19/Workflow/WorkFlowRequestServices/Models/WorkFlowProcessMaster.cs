using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowRequestServices.Models
{
    public class WorkFlowProcessMaster
    {
        [Key]
        public int iProcess_Id { get; set; }

        [Required(ErrorMessage = "Please Enter WorkFlow Process Name")]
        public string sProcess_Name { get; set; }
        public bool bActive { get; set; }
    }
}
