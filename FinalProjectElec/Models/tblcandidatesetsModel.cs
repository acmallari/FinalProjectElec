using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblcandidatesetsModel
    {
        public int candiset_id { get; set; }
        public int candiset_candidateid { get; set; }
        public int candiset_votesetid { get; set; }
        public int archive_status { get; set; }
        public DateTime created_on { get; set; }
        public DateTime? updated_on { get; set; }
    }
}