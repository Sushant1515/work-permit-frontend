using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WorkFlowEntry.Models
{
    public class typeinfo
    {
        [Key]
        public string id { get; set; }
        public string type { get; set; }
    }
}
