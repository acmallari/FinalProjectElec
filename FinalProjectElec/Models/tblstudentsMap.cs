using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tblstudentsMap : EntityTypeConfiguration<tblstudentsModel>
    {
        public tblstudentsMap()
        {
            HasKey(x => x.student_id);
            ToTable("tblstudents");
        }
    }
}