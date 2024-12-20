using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblvotesMap : EntityTypeConfiguration<tblvotesModel>
    {
        public tblvotesMap()
        {
            HasKey(x => x.vote_id);
            ToTable("tblvotes");
        }
    }
}