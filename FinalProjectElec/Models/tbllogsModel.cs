using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tbllogsModel
    {
        public int log_id { get; set; }
        public String log_email { get; set; }
        public int log_studnum { get; set; }
        public String log_action { get; set; }
        public int archive_status { get; set; }
        public DateTime created_on { get; set; }
        public DateTime? updated_on { get; set; }
    }
}