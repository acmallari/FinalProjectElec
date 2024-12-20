using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblvotesetsMap : EntityTypeConfiguration<tblvotesetsModel>
    {
        public tblvotesetsMap()
        {
            HasKey(x => x.set_id);
            ToTable("tblvotesets");
        }
    }
}