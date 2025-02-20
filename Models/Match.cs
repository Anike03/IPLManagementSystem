using Humanizer.Localisation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLManagementSystem.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        [Required]
        public DateTime MatchDate { get; set; }

        // Foreign Key for Venue
        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        // One-to-Many Relationship: A match is played at one venue
        public Venue Venue { get; set; } = null!; // Use null! to suppress null warnings

        // Many-to-Many Relationship: A match involves two teams
        public ICollection<Team> Teams { get; set; } = new List<Team>(); // Initialize to avoid null warnings
    }
}

namespace IPLManagementSystem.DTOs
{
    public class MatchDTO
    {
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int VenueId { get; set; }
        public List<int> TeamIds { get; set; } = new List<int>();
    }
}
