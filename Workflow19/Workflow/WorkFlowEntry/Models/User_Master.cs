using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WorkFlowEntry.Models
{
    public class User_Master
    {
        [Key]
        public decimal iUser_Cd { get; set; }
        public string sUsername { get; set; }
        public string sUser_Full_Name { get; set; }
        public string sPassword { get; set; }
        public Int16 nRole_Id { get; set; }
        public bool bActive { get; set; }
    }
}
