﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    public class tbllogsMap : EntityTypeConfiguration<tbllogsModel>
    {
        public tbllogsMap()
        {
            HasKey(x => x.log_id);
            ToTable("tbllogs");
        }
    }
}