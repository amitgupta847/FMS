using FMS.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.DataAccess
{
    public class FMSContext : DbContext
    {
        public FMSContext( ):base("FMS_Db")
        {
        }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<YearLine> Years { get; set; }

        public DbSet<Deposit> Deposits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // modelBuilder.Entity<Friend>().Property(f => f.FirstName).IsRequired().HasMaxLength(50);
            // another way of using constraint

            //modelBuilder.Configurations.Add(new BanksConfiguration());
        }

        public class BanksConfiguration : EntityTypeConfiguration<Bank>
        {
            public BanksConfiguration()
            {
                //Property(f => f.Name).IsRequired().HasMaxLength(50);
            }
        }

    }
}
