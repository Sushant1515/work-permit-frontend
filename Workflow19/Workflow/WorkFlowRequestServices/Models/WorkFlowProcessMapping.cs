using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlowRequestServices.Models
{
    public class WorkFlowProcessMapping
    {
        [Key]
        public int iWF_Id { get; set; }
       
        [Key]
        public int iProcess_Id { get; set; }
      
        [DataType(DataType.DateTime)]
        public DateTime dCreated_Date { get; set; }
        public int iCreated_User_Cd { get; set; }

       
    }
}
