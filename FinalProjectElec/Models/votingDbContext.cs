using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalProjectElec.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class votingDbContext : DbContext
    {
        static votingDbContext()
        {
            Database.SetInitializer<votingDbContext>(null);
        }

        public votingDbContext() : base("Name=votingdb")
        {

        }

        public virtual DbSet<tblaccountsModel> tblaccounts { get; set; }
        public virtual DbSet<tblcandidatesModel> tblcandidates { get; set; }
        public virtual DbSet<tbllogsModel> tbllogs { get; set; }
        public virtual DbSet<tblpartiesModel> tblparties { get; set; }
        public virtual DbSet<tblpositionsModel> tblpositions { get; set; }
        public virtual DbSet<tblstudentsModel> tblstudents { get; set; }
        public virtual DbSet<tbltalliesModel> tbltallies { get; set; }
        public virtual DbSet<tblvotesetsModel> tblvotesets { get; set; }
        public virtual DbSet<tblvotesModel> tblvotes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new tblaccountsMap());
            modelBuilder.Configurations.Add(new tblcandidatesMap());
            modelBuilder.Configurations.Add(new tbllogsMap());
            modelBuilder.Configurations.Add(new tblpartiesMap());
            modelBuilder.Configurations.Add(new tblpositionsMap());
            modelBuilder.Configurations.Add(new tblstudentsMap());
            modelBuilder.Configurations.Add(new tbltalliesMap());
            modelBuilder.Configurations.Add(new tblvotesetsMap());
            modelBuilder.Configurations.Add(new tblvotesMap());
        }
    }
}