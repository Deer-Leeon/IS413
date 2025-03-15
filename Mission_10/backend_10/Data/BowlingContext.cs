using Microsoft.EntityFrameworkCore;
using backend_10.Models;

namespace backend_10.Data
{
    public class BowlingContext : DbContext
    {
        public BowlingContext(DbContextOptions<BowlingContext> options) 
            : base(options) { }

        // Add this DbSet property for the Bowlers table
        public DbSet<Bowler> Bowlers { get; set; }

        // Add this DbSet property for the Teams table
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Bowler and Team
            modelBuilder.Entity<Bowler>()
                .HasOne(b => b.Team)
                .WithMany()
                .HasForeignKey(b => b.TeamID);
        }
    }
}