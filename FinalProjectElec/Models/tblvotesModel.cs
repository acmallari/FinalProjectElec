using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblvotesModel
    {
        public int vote_id { get; set; }
        public int vote_studid { get; set; }
        public int vote_votesetid { get; set; }
        public String vote_value { get; set; }
        public int archive_status { get; set; }
        public DateTime created_on { get; set; }
        public DateTime? updated_on { get; set; }
    }
}