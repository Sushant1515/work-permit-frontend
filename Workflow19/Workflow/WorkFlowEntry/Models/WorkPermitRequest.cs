using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlowEntry.Models
{
    
    public class WorkPermitRequest
    {
        
        [Key]
        
        [Column("wpid")]
        public string id { get; set; }

        [Column("code")]
        public string code { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please Enter Date")]
        [Column("date")]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Please Enter Status")]
        [StringLength(50, ErrorMessage = "Maximum field  status lenght is 50")]
        [Column("status")]
        public string status { get; set; }

        
        [Required(ErrorMessage = "Please Enter Asset")]
        [StringLength(50, ErrorMessage = "Maximum field asset lenght is 50")]
        [Column("assetid")]
        public string assetid { get; set; }

        
        [Required(ErrorMessage = "Please Enter Title")]
        [StringLength(50, ErrorMessage = "Maximum field title lenght is 50")]
        [Column("title")]
        public string title { get; set; }


        [Required(ErrorMessage = "Please Enter Number")]
        [StringLength(50, ErrorMessage = "Maximum field Number lenght is 50")]
        [Column("number")]
        public string number { get; set; }

        [Required(ErrorMessage = "Please Enter Desciption")]
        [StringLength(200, ErrorMessage = "Maximum field description lenght is 200")]
        [Column("description")]
        public string desciption { get; set; }

        [Required(ErrorMessage = "Please Enter Type")]
        [StringLength(50, ErrorMessage = "Maximum field type lenght is 50")]
        [Column("type")]
        public string type { get; set; }

        [Required(ErrorMessage = "Please Enter Area")]
        [StringLength(50, ErrorMessage = "Maximum field area lenght is 50")]
        [Column("area")]
        public string area { get; set; }

        [Required(ErrorMessage = "Please Enter Subtype")]
        [StringLength(50, ErrorMessage = "Maximum field subtype lenght is 50")]
        [Column("subtype")]
        public string subtype { get; set; }

        [Required(ErrorMessage = "Please Enter Zone")]
        [StringLength(50, ErrorMessage = "Maximum field zone lenght is 50")]
        [Column("zone")]
        public string zone { get; set; }

        [Required(ErrorMessage = "Please Enter Sytem")]
        [StringLength(50, ErrorMessage = "Maximum field sytem lenght is 50")]
        [Column("sytem")]
        public string sytem { get; set; }


        [DataType(DataType.Date)]
        [Column("validfrom")]
        [Required(ErrorMessage = "Please Enter Valid From Date")]
        public DateTime validfrom { get; set; }

        [DataType(DataType.Date)]
        [Column("validto")]
        [Required(ErrorMessage = "Please Enter Valid To Date")]
        public DateTime validto { get; set; }

        [Required(ErrorMessage = "Please Enter Discipline")]
        [StringLength(50, ErrorMessage = "Maximum field discipline lenght is 50")]
        [Column("discipline")]
        public string discipline { get; set; }

        [Required(ErrorMessage = "Please Enter Applicant")]
        [StringLength(50, ErrorMessage = "Maximum field applicant lenght is 50")]
        [Column("applicant")]
        public string applicant { get; set; }

        [Required(ErrorMessage = "Please Enter Safejob")]
        [StringLength(50, ErrorMessage = "Maximum field safejob lenght is 50")]
        [Column("safejob")]
        public string safejob { get; set; }

        [Required(ErrorMessage = "Please Enter Workorder")]
        [StringLength(50, ErrorMessage = "Maximum field workorder lenght is 50")]
        [Column("workorder")]
        public string workorder { get; set; }

        [Required(ErrorMessage = "Please Enter Equipment")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Column("equipment")]
        public int equipment { get; set; }

        [Column("createby")]
        public string createby { get; set; }

        [DataType(DataType.Date)]
        [Column("createdate")]
        public DateTime createdate { get; set; }

        [Column("updateby")]
        public string updateby { get; set; }

        [DataType(DataType.Date)]
        [Column("updatedate")]
        public DateTime updatedate { get; set; }

        [Column("formdata")]
        public string formdata { get; set; }
    }
}
