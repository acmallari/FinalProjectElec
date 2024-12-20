using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblaccountsModel
    {
        public int account_id { get; set; }
        public String account_email { get; set; }
        public String account_pass { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
    }
}