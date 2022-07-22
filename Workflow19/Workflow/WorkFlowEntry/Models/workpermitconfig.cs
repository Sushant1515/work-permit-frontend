using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WorkFlowEntry.Models
{
    public class workpermitconfig
    {
        [Key]
       
        public string id { get; set; }

      
        public string formtext { get; set; }
    }
}
