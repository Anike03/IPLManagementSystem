using IPLManagementSystem.Data;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Interfaces;
using IPLManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IPLManagementSystem.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext _context;

        public TeamService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Team> AllTeams => _context.Teams.ToList();

        public Team? GetTeamById(int id)
        {
            return _context.Teams
                .Include(t => t.Players)
                .Include(t => t.Matches)
                .FirstOrDefault(t => t.TeamId == id);
        }

        public void CreateTeam(TeamDTO teamDTO)
        {
            var team = new Team
            {
                TeamName = teamDTO.Name,
                Coach = teamDTO.Coach
            };

            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        public void UpdateTeam(int id, TeamDTO teamDTO)
        {
            var team = _context.Teams.Find(id) ?? throw new KeyNotFoundException("Team not found");

            team.TeamName = teamDTO.Name;
            team.Coach = teamDTO.Coach;

            _context.Teams.Update(team);
            _context.SaveChanges();
        }

        public void DeleteTeam(int id)
        {
            var team = _context.Teams.Find(id);
            if (team == null)
                throw new KeyNotFoundException("Team not found");

            _context.Teams.Remove(team);
            _context.SaveChanges();
        }
        public IEnumerable<Team> GetAllTeams()
        {
            return [.. _context.Teams]; // Simplified collection initialization
        }
    }
}