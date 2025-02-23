using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using System.Collections.Generic;

namespace IPLManagementSystem.Interfaces
{
    public interface IPlayerService
    {
        IEnumerable<Player> GetAllPlayers();
        Player? GetPlayerById(int id);
        void CreatePlayer(PlayerDTO playerDTO);
        void UpdatePlayer(int id, PlayerDTO playerDTO);
        void DeletePlayer(int id);
    }
}