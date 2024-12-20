using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblvotesetsModel
    {
        public int set_id { get; set; }
        public int set_studid { get; set; }
        public int set_num { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
    }
}