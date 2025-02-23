using IPLManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IPLManagementSystem.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity tables are configured properly

            // Many-to-Many Relationship: Match ↔ Teams
            modelBuilder.Entity<Match>()
                .HasMany(m => m.Teams)
                .WithMany(t => t.Matches)
                .UsingEntity<Dictionary<string, object>>(
                    "MatchTeams",
                    j => j.HasOne<Team>().WithMany().HasForeignKey("TeamId"),
                    j => j.HasOne<Match>().WithMany().HasForeignKey("MatchId"),
                    j =>
                    {
                        j.HasKey("MatchId", "TeamId");
                    }
                );

            // One-to-Many Relationship: Venue ↔ Matches
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Venue)
                .WithMany(v => v.Matches)
                .HasForeignKey(m => m.VenueId)
                .OnDelete(DeleteBehavior.Cascade); // Ensures if a Venue is deleted, its Matches are removed
        }
    }
}
