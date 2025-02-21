using System;
using System.Collections.Generic;

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