using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblcandidatesMap : EntityTypeConfiguration<tblcandidatesModel>
    {
        public tblcandidatesMap()
        {
            HasKey(x => x.candidate_id);
            ToTable("tblcandidates");
        }
    }
}