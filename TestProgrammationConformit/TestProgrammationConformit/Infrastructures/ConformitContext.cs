using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Models;

namespace TestProgrammationConformit.Infrastructures
{
    public class ConformitContext : DbContext
    {
        public DbSet<Evenement> Evenements { get; set; }

        public ConformitContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evenement>().ToTable("Evenement");
        }


        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
