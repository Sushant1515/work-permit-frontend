using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WorkFlowEntry.Models
{
    public class assetinfo
    {
        [Key]
        public string id { get; set; }
        public string asset { get; set; }
    }
}
