using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Models;
using IPLManagementSystem.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IPLManagementSystem.Data;

namespace IPLManagementSystem.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Player
        public IActionResult Index()
        {
            var players = _context.Players
                .Include(p => p.Team)
                .ToList();
            return View(players);
        }

        // GET: Player/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = _context.Players
                .Include(p => p.Team)
                .FirstOrDefault(p => p.PlayerId == id);

            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlayerDTO playerDTO)
        {
            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Teams = _context.Teams.ToList();
            return View(playerDTO);
        }

        // GET: Player/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = _context.Players.Find(id);

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

            ViewBag.Teams = _context.Teams.ToList();
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
                var player = _context.Players.Find(id);

                if (player == null)
                {
                    return NotFound();
                }

                player.Name = playerDTO.Name;
                player.Age = playerDTO.Age;
                player.Role = playerDTO.Role;
                player.TeamId = playerDTO.TeamId;

                _context.Update(player);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Teams = _context.Teams.ToList();
            return View(playerDTO);
        }

        // GET: Player/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = _context.Players
                .Include(p => p.Team)
                .FirstOrDefault(p => p.PlayerId == id);

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
            var player = _context.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}