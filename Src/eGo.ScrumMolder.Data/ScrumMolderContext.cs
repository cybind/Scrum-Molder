using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using eGo.ScrumMolder.Dto;
using System.Data;

using eGo.ScrumMolder.Dto.Bugs;

namespace eGo.ScrumMolder.Data
{
    public class ScrumMolderContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<DailyScrum> DailyScrums { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<DailyProjectScrum> DailyProjectScrums { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }
        
        public DbSet<Bug> Bugs { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<BugHistory> BugHistories { get; set; }



        public DataTable ScrumDailyReport { get; set; }

        public ScrumMolderContext() 
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ScrumMolderContext>());
            Database.SetInitializer(new ScrumMolderContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bug>().HasOptional(a => a.Category);

            modelBuilder.Entity<Bug>()
                .HasOptional(a => a.AssignedTo)
                .WithMany()
                .HasForeignKey(u => u.AssignedToId);

            modelBuilder.Entity<Bug>()
                .HasRequired(a => a.CreatedBy)
                .WithMany()
                .HasForeignKey(u => u.CreatedById);
        }
      
    }
}
