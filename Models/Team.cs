using System.Numerics;

namespace IPLManagementSystem.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}

namespace IPLManagementSystem.DTOs
{
    public class TeamDTO
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;
    }
}
