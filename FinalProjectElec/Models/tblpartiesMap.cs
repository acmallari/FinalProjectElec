using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblpartiesMap : EntityTypeConfiguration<tblpartiesModel>
    {
        public tblpartiesMap()
        {
            HasKey(x => x.party_id);
            ToTable("tblparties");
        }
    }
}