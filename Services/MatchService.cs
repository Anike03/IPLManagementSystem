using IPLManagementSystem.Data;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Interfaces;
using IPLManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IPLManagementSystem.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _context;

        public MatchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Match> GetAllMatches()
        {
            return _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .ToList();
        }

        public Match? GetMatchById(int id)
        {
            return _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .FirstOrDefault(m => m.MatchId == id);
        }

        public void CreateMatch(MatchDTO matchDTO)
        {
            var match = new Match
            {
                MatchDate = matchDTO.MatchDate,
                VenueId = matchDTO.VenueId,
                Teams = _context.Teams.Where(t => matchDTO.TeamIds.Contains(t.TeamId)).ToList()
            };

            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        public void UpdateMatch(int id, MatchDTO matchDTO)
        {
            var match = _context.Matches
                .Include(m => m.Teams)
                .FirstOrDefault(m => m.MatchId == id) ?? throw new KeyNotFoundException("Match not found");

            match.MatchDate = matchDTO.MatchDate;
            match.VenueId = matchDTO.VenueId;
            match.Teams = [.. _context.Teams.Where(t => matchDTO.TeamIds.Contains(t.TeamId))];

            _context.Matches.Update(match);
            _context.SaveChanges();
        }

        public void DeleteMatch(int id)
        {
            var match = _context.Matches.Find(id);
            if (match == null)
                throw new KeyNotFoundException("Match not found");

            _context.Matches.Remove(match);
            _context.SaveChanges();
        }

        public List<Venue> GetVenues()
        {
            return [.. _context.Venues]; // Simplified collection initialization
        }

        public List<Team> GetTeams()
        {
            return [.. _context.Teams]; // Simplified collection initialization
        }
    }
}