using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IPLManagementSystem.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // Initialize to avoid null warnings

        [Required]
        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = string.Empty; // Initialize to avoid null warnings

        // Foreign Key for Team
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        // One-to-Many Relationship: A player belongs to one team
        public Team Team { get; set; } = null!; // Use null! to suppress null warnings
    }
}

