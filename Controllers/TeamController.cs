using Microsoft.AspNetCore.Mvc;
using IPLManagementSystem.Models;
using IPLManagementSystem.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IPLManagementSystem.Data;

namespace IPLManagementSystem.Controllers
{
    public class TeamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Team
        public IActionResult Index()
        {
            var teams = _context.Teams.ToList();
            return View(teams);
        }

        // GET: Team/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = _context.Teams
                .Include(t => t.Players)
                .Include(t => t.Matches)
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Team/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeamDTO teamDTO)
        {
            if (ModelState.IsValid)
            {
                var team = new Team
                {
                    TeamName = teamDTO.Name,
                    Coach = teamDTO.Coach
                };

                _context.Teams.Add(team);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(teamDTO);
        }

        // GET: Team/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = _context.Teams.Find(id);

            if (team == null)
            {
                return NotFound();
            }

            var teamDTO = new TeamDTO
            {
                TeamId = team.TeamId,
                Name = team.TeamName,
                Coach = team.Coach
            };

            return View(teamDTO);
        }

        // POST: Team/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TeamDTO teamDTO)
        {
            if (id != teamDTO.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var team = _context.Teams.Find(id);

                if (team == null)
                {
                    return NotFound();
                }

                team.TeamName = teamDTO.Name;
                team.Coach = teamDTO.Coach;

                _context.Update(team);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(teamDTO);
        }

        // GET: Team/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = _context.Teams
                .Include(t => t.Players)
                .Include(t => t.Matches)
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var team = _context.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}