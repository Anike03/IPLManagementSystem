using IPLManagementSystem.Data;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Interfaces;
using IPLManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IPLManagementSystem.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;

        public PlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _context.Players
                .Include(p => p.Team)
                .ToList();
        }

        public Player? GetPlayerById(int id)
        {
            return _context.Players
                .Include(p => p.Team)
                .FirstOrDefault(p => p.PlayerId == id);
        }

        public void CreatePlayer(PlayerDTO playerDTO)
        {
            var player = new Player
            {
                Name = playerDTO.Name,
                Age = playerDTO.Age,
                Role = playerDTO.Role,
                TeamId = playerDTO.TeamId
            };

            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public void UpdatePlayer(int id, PlayerDTO playerDTO)
        {
            var player = _context.Players.Find(id) ?? throw new KeyNotFoundException("Player not found");

            player.Name = playerDTO.Name;
            player.Age = playerDTO.Age;
            player.Role = playerDTO.Role;
            player.TeamId = playerDTO.TeamId;

            _context.Players.Update(player);
            _context.SaveChanges();
        }

        public void DeletePlayer(int id)
        {
            var player = _context.Players.Find(id);
            if (player == null)
                throw new KeyNotFoundException("Player not found");

            _context.Players.Remove(player);
            _context.SaveChanges();
        }

        public List<Team> GetTeams()
        {
            return [.. _context.Teams]; // Simplified collection initialization
        }
    }
}