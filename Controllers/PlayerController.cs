using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Interfaces;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Models;
using IPLManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IPLManagementSystem.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly ApplicationDbContext _context;

        public PlayerController(IPlayerService playerService, ApplicationDbContext context)
        {
            _playerService = playerService;
            _context = context;
        }

        public IActionResult Index()
        {
            var players = _playerService.GetAllPlayers();
            return View(players);
        }

        public IActionResult Details(int id)
        {
            var player = _playerService.GetPlayerById(id);
            if (player == null)
                return NotFound();

            return View(player);
        }

        public IActionResult Create()
        {
            ViewBag.Teams = new SelectList(_context.Teams, "TeamId", "TeamName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(PlayerDTO playerDTO)
        {
            if (ModelState.IsValid)
            {
                _playerService.CreatePlayer(playerDTO);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Teams = new SelectList(_context.Teams, "TeamId", "TeamName");
            return View(playerDTO);
        }

        public IActionResult Edit(int id)
        {
            var player = _playerService.GetPlayerById(id);
            if (player == null)
                return NotFound();

            // Map Player to PlayerDTO
            var playerDTO = new PlayerDTO
            {
                PlayerId = player.PlayerId,
                Name = player.Name,
                Age = player.Age,
                Role = player.Role,
                TeamId = player.TeamId
            };

            ViewBag.Teams = new SelectList(_context.Teams, "TeamId", "TeamName");
            return View(playerDTO);
        }

        [HttpPost]
        public IActionResult Edit(int id, PlayerDTO playerDTO)
        {
            if (ModelState.IsValid)
            {
                _playerService.UpdatePlayer(id, playerDTO);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Teams = new SelectList(_context.Teams, "TeamId", "TeamName");
            return View(playerDTO);
        }

        public IActionResult Delete(int id)
        {
            var player = _playerService.GetPlayerById(id);
            if (player == null)
                return NotFound();

            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _playerService.DeletePlayer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}