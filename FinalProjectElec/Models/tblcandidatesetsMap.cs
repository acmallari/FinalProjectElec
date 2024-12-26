using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblcandidatesetsMap : EntityTypeConfiguration<tblcandidatesetsModel>
    {
        public tblcandidatesetsMap()
        {
            HasKey(x => x.candiset_id);
            ToTable("tblcandidatesets");
        }
    }
}