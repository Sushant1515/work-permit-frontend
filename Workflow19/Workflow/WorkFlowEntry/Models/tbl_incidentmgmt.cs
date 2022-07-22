using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WorkFlowEntry.Models
{
    public class tbl_incidentmgmt
    {
        [Key]

        [Column("inci_id")]
        public string id { get; set; }

        [Required(ErrorMessage = "Please Enter Title")]
        [StringLength(50, ErrorMessage = "Please Enter Title Define Length")]
        [Column("inci_title")]
        public string title { get; set; }

        [DataType(DataType.Date)]
        [Column("inci_date")]
        public DateTime incdate { get; set; }


        [Required(ErrorMessage = "Please Enter Asset")]
        [StringLength(50, ErrorMessage = "Please Enter Define Length")]
        [Column("inci_location")]
        public string location { get; set; }


        [Column("inci_crby")]
        public string createdby { get; set; }

        [DataType(DataType.Date)]
        [Column("inci_crdate")]
        public DateTime createdate { get; set; }

        [Column("inci_upby")]
        public string updateby { get; set; }

        [DataType(DataType.Date)]
        [Column("inci_update")]
        public DateTime updatedate { get; set; }

        [Column("inci_status")]
        [Required(ErrorMessage = "Please Enter Status")]
        [StringLength(10, ErrorMessage = "Please Enter Status Define Length")]
        public string status { get; set; }

        
        [Column("inci_substatus")]
        [Required(ErrorMessage = "Please Enter Sub Status")]
        [StringLength(10, ErrorMessage = "Please Enter Sub Status Define Length")]
        public string substatus { get; set; }
    }
}
