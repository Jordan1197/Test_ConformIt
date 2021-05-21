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
        public DbSet<Evenement> evenement { get; set; }
        public DbSet<Commentaire> commentaire { get; set; }

        public ConformitContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evenement>().ToTable("evenement");
            modelBuilder.Entity<Commentaire>().ToTable("commentaire");
            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
