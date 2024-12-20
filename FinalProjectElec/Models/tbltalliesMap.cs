using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tbltalliesMap : EntityTypeConfiguration<tbltalliesModel>
    {
        public tbltalliesMap()
        {
            HasKey(x => x.tally_id);
            ToTable("tbltallies");
        }
    }
}