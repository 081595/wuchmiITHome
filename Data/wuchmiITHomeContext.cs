using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wuchmiITHome.Models;

namespace wuchmiITHome.Data
{
    public class wuchmiITHomeContext: DbContext
    {
        public wuchmiITHomeContext (
            DbContextOptions<wuchmiITHomeContext> options)
            : base(options)
        {
        }

        public DbSet<wuchmiITHome.Models.Article> Article { get; set; }
        public DbSet<TeachAppoEmployee> TeachAppoEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite primary key for TeachAppoEmployee
            modelBuilder.Entity<TeachAppoEmployee>()
                .HasKey(e => new { e.Yr, e.IdNo, e.Birthday });

            // Configure SQLite defaults for timestamps
            modelBuilder.Entity<TeachAppoEmployee>()
                .Property(e => e.CreateDate)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<TeachAppoEmployee>()
                .Property(e => e.UpdateDate)
                .HasDefaultValueSql("datetime('now')");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Automatically set CreateDate and UpdateDate timestamps
            var entries = ChangeTracker.Entries<TeachAppoEmployee>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = DateTime.UtcNow;
                    entry.Entity.UpdateDate = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdateDate = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}