using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowRequestServices.Models
{
    public class WorkFlowMaster
    {
        [Key]
        public int iWF_Id { get; set; }

        [Required(ErrorMessage = "Please Enter WorkFlow Description")]
        public string sWF_Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime dWF_Create_Date { get; set; }
        public int iCreated_User_Cd { get; set; }
        public bool bActive { get; set; }
    }
}
