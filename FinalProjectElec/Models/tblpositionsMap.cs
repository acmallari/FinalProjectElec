using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblpositionsMap : EntityTypeConfiguration<tblpositionsModel>
    {
        public tblpositionsMap()
        {
            HasKey(x => x.position_id);
            ToTable("tblpositions");
        }
    }
}