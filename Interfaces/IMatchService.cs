using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using System.Collections.Generic;

namespace IPLManagementSystem.Interfaces
{
    public interface IMatchService
    {
        IEnumerable<Match> GetAllMatches();
        Match? GetMatchById(int id);
        void CreateMatch(MatchDTO matchDTO);
        void UpdateMatch(int id, MatchDTO matchDTO);
        void DeleteMatch(int id);
    }
}