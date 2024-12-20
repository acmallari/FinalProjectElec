using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblaccountsMap : EntityTypeConfiguration<tblaccountsModel>
    {
        public tblaccountsMap()
        {
            HasKey(x => x.account_id);
            ToTable("tblaccounts");
        }
    }
}