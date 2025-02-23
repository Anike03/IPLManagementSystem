using System.ComponentModel.DataAnnotations;

namespace IPLManagementSystem.Models
{
    public class Venue
    {
        internal object Id;

        [Key]
        public int VenueId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // Initialize to avoid null warnings

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty; // Initialize to avoid null warnings

        // One-to-Many Relationship: A venue can host many matches
        public ICollection<Match> Matches { get; set; } = new List<Match>(); // Initialize to avoid null warnings
    }
}

