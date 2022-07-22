using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlowEntry.Models
{
    public class tbl_image
    {
        [Key]
        [Column("imgid")]
        public string id { get; set; }
        [Column("imgdate")]
        public DateTime imgdate { get; set; }
        [Column("imgname")]
        public string imgname { get; set; }
        [Column("imgpath")]
        public string imgpath { get; set; }
    }
}
