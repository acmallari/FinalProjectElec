using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblcandidatesModel
    {
        public int candidate_id { get; set; }
        public String candidate_fname { get; set; }
        public String candidate_lname { get; set; }
        public int candidate_partyid { get; set; }
        public int candidate_positionid { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
    }
}