using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblpartiesModel
    {
        public int party_id { get; set; }
        public String party_name { get; set; }
        public String party_campaign { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
    }
}