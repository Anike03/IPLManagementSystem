using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Models;
using IPLManagementSystem.DTOs;
using IPLManagementSystem.Interfaces;  // Reference the Interfaces folder
using System.Linq;

namespace IPLManagementSystem.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: Player
        public IActionResult Index()
        {
            var players = _playerService.GetAllPlayers();
            return View(players);
        }

        // GET: Player/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = _playerService.GetPlayerById(id.Value);

            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            ViewBag.Teams = _playerService.GetTeams();
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlayerDTO playerDTO)
        {
            if (ModelState.IsValid)
            {
                _playerService.CreatePlayer(playerDTO);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Teams = _playerService.GetTeams();
            return View(playerDTO);
        }

        // GET: Player/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = _playerService.GetPlayerById(id.Value);

            if (player == null)
            {
                return NotFound();
            }

            var playerDTO = new PlayerDTO
            {
                PlayerId = player.PlayerId,
                Name = player.Name,
                Age = player.Age,
                Role = player.Role,
                TeamId = player.TeamId
            };

            ViewBag.Teams = _playerService.GetTeams();
            return View(playerDTO);
        }

        // POST: Player/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PlayerDTO playerDTO)
        {
            if (id != playerDTO.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _playerService.UpdatePlayer(id, playerDTO);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Teams = _playerService.GetTeams();
            return View(playerDTO);
        }

        // GET: Player/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = _playerService.GetPlayerById(id.Value);

            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _playerService.DeletePlayer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}