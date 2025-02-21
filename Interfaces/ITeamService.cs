using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using System.Collections.Generic;

namespace IPLManagementSystem.Interfaces
{
    public interface ITeamService
    {
        IEnumerable<Team> AllTeams { get; }

        Team? GetTeamById(int id); // Add nullable return type
        void CreateTeam(TeamDTO teamDTO);
        void UpdateTeam(int id, TeamDTO teamDTO);
        void DeleteTeam(int id);
    }
}