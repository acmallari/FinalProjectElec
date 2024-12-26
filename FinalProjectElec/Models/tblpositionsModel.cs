using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblpositionsModel
    {
        public int position_id { get; set; }
        public String position_name { get; set; }
        public int archive_status { get; set; }
        public DateTime created_on { get; set; }
        public DateTime? updated_on { get; set; }
    }
}