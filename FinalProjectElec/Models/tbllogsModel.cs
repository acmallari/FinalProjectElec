using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tbllogsModel
    {
        public int log_id { get; set; }
        public int log_accountid { get; set; }
        public String log_action { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
    }
}