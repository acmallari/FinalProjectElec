using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tbltalliesModel
    {
        public int tally_id { get; set; }
        public int tally_candidateid { get; set; }
        public int tally_value { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
    }
}